using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleSoapClient
{
    public class XElementSerializer : IXElementSerializer
    {
        /// <summary>
        ///     Serializes the specified object to XElement
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        public XElement Serialize(object obj, string nsText = "", string nsUrl = "")
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if(nsText != null && nsUrl != null)
            {
                ns.Add(nsText, nsUrl);
            }
            
            var xs = new XmlSerializer(obj.GetType());

            var xDoc = new XDocument();

            using (var xw = xDoc.CreateWriter())
                    xs.Serialize(xw, obj, ns);

            return xDoc.Root;
        }
    }
}