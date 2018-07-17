using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleSoapClient
{
    public class SoapClient : ISoapClient, IDisposable
    {
        private const string Body = "Body";
        private const string Header = "Header";
        private const string Prefix = "soap";
        private const string Envelope = "Envelope";
        private const string ActionHeader = "SOAPAction";
        private const string ActionParameter = "action";

        private readonly HttpClient _httpClient;
        private readonly string _mediaType;
        private readonly XNamespace _soapSchema;
        private readonly SoapVersion _version;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoapClient" /> class.
        /// </summary>
        /// <param name="httpClientFactory">Allows the user to define the construction of the HttpClient</param>
        /// <param name="version">Optionally provide a preferred SOAP version. Defaults to SOAP 1.1.</param>
        private SoapClient(Func<HttpClient> httpClientFactory, SoapVersion version = SoapVersion.Soap11)
        {
            _version = version;

            switch (version)
            {
                case SoapVersion.Soap11:
                    _soapSchema = "http://schemas.xmlsoap.org/soap/envelope/";
                    _mediaType = "text/xml";
                    break;
                case SoapVersion.Soap12:
                    _soapSchema = "http://www.w3.org/2003/05/soap-envelope";
                    _mediaType = "application/soap+xml";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(version), version,
                        $"The {version.GetType().Name} enum contains an unsupported value.");
            }

            _httpClient = httpClientFactory();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoapClient" /> class.
        ///     The internal HttpClient supports AutomaticDecompression of GZip and Deflate
        /// </summary>
        public SoapClient(SoapVersion version = SoapVersion.Soap11) : this(DefaultHttpFactory, version)
        {
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        ///     Posts an asynchronous message.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message.</param>
        /// <param name="header">The header of the SOAP message.</param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public Task<HttpResponseMessage> PostAsync(string endpoint, XElement body, XElement header = null,
            string action = null, string username = null, string password = null,string nstext = null, string nsurl = null)
        {
            if (body == null)
                throw new ArgumentNullException(Body);

            var soapMessage = GetSoapMessage(header, body, nstext, nsurl);
            var content = new StringContent(soapMessage, Encoding.UTF8, _mediaType);

            if(username != null && password != null)
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password)));
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            }

            if (action == null)
                return _httpClient.PostAsync(endpoint, content);

            if (_version == SoapVersion.Soap11)
                content.Headers.Add(ActionHeader, action);

            if (_version == SoapVersion.Soap12)
                content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue(ActionParameter, $"\"{action}\""));

           
            return _httpClient.PostAsync(endpoint, content);
        }

        #region Private Methods

        private static HttpClient DefaultHttpFactory()
        {
            var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            return client;
        }

        private string GetSoapMessage(XElement header, XElement body, string nstext, string nsurl)
        {
            var soapMessage = new XElement(_soapSchema + Envelope,
                new XAttribute(XNamespace.Xmlns + Prefix, _soapSchema.NamespaceName)//,
                //new XAttribute(XNamespace.Xmlns + nstext, nsurl) 
                );

            if (header != null)
                soapMessage.Add(new XElement(_soapSchema + Header, header));

            soapMessage.Add(new XElement(_soapSchema + Body, body));

            return new XElement(soapMessage).ToString();
        }

        #endregion Private Methods
    }
}