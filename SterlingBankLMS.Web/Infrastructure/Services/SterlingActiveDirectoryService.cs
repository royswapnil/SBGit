using SimpleSoapClient;
using SimpleSoapClient.Extensions;
using SterlingBankLMS.Web.Utilities;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public interface ISterlingActiveDirectoryService
    {
        Task<bool> ValidateUser(string email, string password);
    }

    public class SterlingActiveDirectoryService : ISterlingActiveDirectoryService
    {
        private string _url => CommonHelper.GetAppSetting<string>(AppConstants.Keys.ActiveDirectoryUrlKey);
        private string _action => CommonHelper.GetAppSetting<string>(AppConstants.Keys.ActionKey);

        private readonly ISoapClient _soapClient;
        public SterlingActiveDirectoryService(ISoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var response = false;

            try {

                var model = new ValidateAD { Email = email, Password = password };

                var responseMsg = _soapClient.Post(_url, model, action: _action);

                responseMsg.EnsureSuccessStatusCode();

                var responseString = await responseMsg.Content.ReadAsStringAsync();

                var validateResult = DeserializeInnerSoapObject<ValidateADResponse>(responseString);

                bool.TryParse(validateResult.ValidateADResult, out response);
            }
            catch (Exception) {

            }

            return response;
        }

        private static T DeserializeInnerSoapObject<T>(string soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapResponse);

            var soapBody = xmlDocument.GetElementsByTagName("soap:Body")[0];
            string innerObject = soapBody.InnerXml;

            XmlSerializer deserializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(RemoveNamespaces(innerObject))) {
                return (T) deserializer.Deserialize(reader);
            }
        }

        public static string RemoveNamespaces(string oldXml)
        {

            XDocument newXml = XDocument.Parse(Regex.Replace(
                oldXml,
                @"(xmlns:?[^=]*=[""][^""]*[""])",
                "",
                RegexOptions.IgnoreCase | RegexOptions.Multiline)
            );
            return newXml.ToString();
        }
    }


    [Serializable]
    [XmlRoot(Namespace = "http://tempuri.org/")]
    public class ValidateAD
    {
        [XmlElement(ElementName ="email")]
        public string Email { get; set; }
        [XmlElement(ElementName ="password")]
        public string Password { get; set; }
    }

    [Serializable]
    public class ValidateADResponse
    {
        public string ValidateADResult { get; set; }
    }
}