using System.Xml.Linq;

namespace SimpleSoapClient
{
    public interface IXElementSerializer
    {
        /// <summary>
        ///     Serializes the specified object to XElement
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        XElement Serialize(object obj, string nsText = null, string nsUrl = null);
    }
}