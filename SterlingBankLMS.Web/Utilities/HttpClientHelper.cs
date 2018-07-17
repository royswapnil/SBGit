using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SterlingBankLMS.Web.Utilities
{
    public class HttpClientHelper
    {
        public static async Task<T> Post<T>(string url, Dictionary<string, string> body, T defValue = default(T))
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            using (var client = new HttpClient()) {

                var result = await client.PostAsync(url, new FormUrlEncodedContent(body));

                result.EnsureSuccessStatusCode();

                if (result.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> contentType)) {

                    if (IsXml(contentType)) {

                        var stream = await result.Content.ReadAsStreamAsync();
                        var serializer = new XmlSerializer(typeof(T));
                        return (T) serializer.Deserialize(stream);
                    }

                    var jsonresult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonresult); ;
                }

                return defValue;
            }
        }

        public static async Task<T> PostSoap<T>(string url, Dictionary<string, string> body, T defValue = default(T))
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            using (var client = new HttpClient()) {

                client.DefaultRequestHeaders.Add("SOAPAction", "http://localhost/teek_api/service.php/ping");
                var content = new StringContent("text/xml; charset=utf-8");

                var result = await client.PostAsync(url, new FormUrlEncodedContent(body));

                result.EnsureSuccessStatusCode();

                if (result.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> contentType)) {

                    if (IsXml(contentType)) {

                        var stream = await result.Content.ReadAsStreamAsync();
                        var serializer = new XmlSerializer(typeof(T));
                        return (T) serializer.Deserialize(stream);
                    }

                    var jsonresult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonresult); ;
                }

                return defValue;
            }
        }


        private static bool IsXml(IEnumerable<string> contentType)
        {
            var xmlHeader = contentType.FirstOrDefault();
            return !string.IsNullOrWhiteSpace(xmlHeader) && xmlHeader.Contains("text/xml");
        }

        public static async Task<T> Get<T>(string url, T defValue = default(T))
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            using (var client = new HttpClient()) {

                var result = await client.GetAsync(url);

                result.EnsureSuccessStatusCode();

                if (result.Content.Headers.TryGetValues("Content-Type", out IEnumerable<string> contentType)) {

                    if (IsXml(contentType)) {

                        var stream = await result.Content.ReadAsStreamAsync();
                        var serializer = new XmlSerializer(typeof(T));
                        return (T) serializer.Deserialize(stream);
                    }

                    var jsonresult = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonresult); ;
                }

                return defValue;
            }
        }
    }
}