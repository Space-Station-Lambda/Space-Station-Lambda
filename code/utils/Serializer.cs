using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SSL.utils
{
    /// <summary>
    /// A serializer provides basic serializer and materializer methods.
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// Deserializes an XML string to a new object.
        /// </summary>
        /// <param name="xml">The XML object to materialize.</param>
        /// <param name="encoding">Encoding of the deserialization, UTF-8 by default.</param>
        /// <typeparam name="T">Type of the generated object.</typeparam>
        /// <returns>Generated Object.</returns>
        public T Deserialize<T>(string xml, string encoding = "utf-8") where T : class
        {
            using MemoryStream memoryStream = new(Encoding.GetEncoding(encoding).GetBytes(xml));
            using XmlTextReader xmlr = new(memoryStream);
            DataContractSerializer serializer = new(typeof(T));
            return (T) serializer.ReadObject(xmlr);
        }

        /// <summary>
        /// Serializes the object to an XML string.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <param name="encoding">Encoding of the serialization, UTF-8 by default.</param>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <returns>Serialized XML String.</returns>
        public string Serialize<T>(T objectToSerialize, string encoding = "utf-8") where T : class
        {
            DataContractSerializer serializer = new(typeof(T));
            using MemoryStream memoryStream = new();
            using XmlTextWriter xmlw = new(memoryStream, Encoding.GetEncoding(encoding));
            xmlw.Formatting = Formatting.Indented;
            serializer.WriteObject(xmlw, objectToSerialize);
            xmlw.Flush();
            return Encoding.GetEncoding(encoding).GetString(memoryStream.ToArray());
        }
    }
}
