using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleSoapClient.Extensions
{
    public static class SoapClientExtensions
    {
        /// <summary>
        ///     Posts a message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message.</param>
        /// <param name="header">The header of the SOAP message.</param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static Task<HttpResponseMessage> PostAsync(
            this ISoapClient client,
            Uri endpoint,
            XElement body,
            XElement header = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return client.PostAsync(endpoint.ToString(), body, header, action, username, password, nstext, nsurl);
        }

        /// <summary>
        ///     Posts a message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message.</param>
        /// <param name="header">The header of the SOAP message.</param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static HttpResponseMessage Post(
            this ISoapClient client,
            string endpoint,
            XElement body,
            XElement header = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return Task.Run(() => client.PostAsync(endpoint, body, header, action,username,password, nstext, nsurl)).Result;
        }

        /// <summary>
        ///     Posts a message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message.</param>
        /// <param name="header">The header of the SOAP message.</param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static HttpResponseMessage Post(
            this ISoapClient client,
            Uri endpoint,
            XElement body,
            XElement header = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return Task.Run(() => client.PostAsync(endpoint.ToString(), body, header, action, username, password, nstext, nsurl)).Result;
        }

        /// <summary>
        ///     Posts an asynchronous message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message which will be serialized to XElement.</param>
        /// <param name="header">The header of the SOAP message which wil be serialized to XElement.</param>
        /// <param name="xElementSerializerFactory">
        ///     Allows the user to define a custom IXElementSerializer instance which will be
        ///     used for serialization
        /// </param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static Task<HttpResponseMessage> PostAsync(
            this ISoapClient client,
            string endpoint,
            object body,
            object header = null,
            Func<IXElementSerializer> xElementSerializerFactory = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null, 
            string nsurl = null)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if (xElementSerializerFactory == null)
                xElementSerializerFactory = () => new XElementSerializer();

            var xElementSerializer = xElementSerializerFactory();

            var headerElement = default(XElement);

            if (header != null)
                headerElement = xElementSerializer.Serialize(header,nstext,nsurl);

            return client.PostAsync(endpoint, xElementSerializer.Serialize(body, nstext, nsurl), headerElement, action, username, password, nstext, nsurl);
        }

        /// <summary>
        ///     Posts an asynchronous message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message which will be serialized to XElement.</param>
        /// <param name="header">The header of the SOAP message which wil be serialized to XElement.</param>
        /// <param name="xElementSerializerFactory">
        ///     Allows the user to define a custom IXElementSerializer instance which will be
        ///     used for serialization
        /// </param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static Task<HttpResponseMessage> PostAsync(
            this ISoapClient client,
            Uri endpoint,
            object body,
            object header = null,
            Func<IXElementSerializer> xElementSerializerFactory = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return client.PostAsync(endpoint.ToString(), body, header, xElementSerializerFactory, action, username, password, nstext, nsurl);
        }

        /// <summary>
        ///     Posts a message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message which will be serialized to XElement.</param>
        /// <param name="header">The header of the SOAP message which wil be serialized to XElement.</param>
        /// <param name="xElementSerializerFactory">
        ///     Allows the user to define a custom IXElementSerializer instance which will be
        ///     used for serialization
        /// </param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static HttpResponseMessage Post(
            this ISoapClient client,
            string endpoint,
            object body,
            object header = null,
            Func<IXElementSerializer> xElementSerializerFactory = null,
            string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return Task.Run(() => client.PostAsync(endpoint, body, header, xElementSerializerFactory, action, username, password, nstext,nsurl)).Result;
        }

        /// <summary>
        ///     Posts a message.
        /// </summary>
        /// <param name="client">Instance of SoapClient</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="body">The body of the SOAP message which will be serialized to XElement.</param>
        /// <param name="header">The header of the SOAP message which wil be serialized to XElement.</param>
        /// <param name="xElementSerializerFactory">
        ///     Allows the user to define a custom IXElementSerializer instance which will be
        ///     used for serialization
        /// </param>
        /// <param name="action"></param>
        /// <param name="username">The SOAP Basic Authentication username.</param>
        /// <param name="password">The SOAP Basic Authentication password.</param>
        public static HttpResponseMessage Post(
            this ISoapClient client,
            Uri endpoint,
            object body,
            object header = null,
            Func<IXElementSerializer> xElementSerializerFactory = null,
           string action = null,
            string username = null,
            string password = null,
            string nstext = null,
            string nsurl = null)
        {
            return client.Post(endpoint.ToString(), body, header, xElementSerializerFactory, action, username, password, nstext, nsurl);
        }
    }
}