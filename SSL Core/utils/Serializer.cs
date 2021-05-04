using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SSL_Core.utils
{
    public class Serializer
    {
        public T Deserialize<T>(string xml, string encoding) where T : class
        {
            using MemoryStream memoryStream = new(Encoding.GetEncoding(encoding).GetBytes(xml));
            using XmlTextReader xmlr = new(memoryStream);
            DataContractSerializer serializer = new(typeof(T));
            return (T) serializer.ReadObject(xmlr);
        }

        public string Serialize<T>(T objectToSerialize, string encoding = "utf-8", bool indented = true) where T : class
        {
            DataContractSerializer serializer = new(typeof(T));
            using MemoryStream memoryStream = new();
            using XmlTextWriter xmlw = new(memoryStream, Encoding.GetEncoding(encoding));
            xmlw.Formatting = indented ? Formatting.Indented : Formatting.None;
            serializer.WriteObject(xmlw, objectToSerialize);
            xmlw.Flush();
            return Encoding.GetEncoding(encoding).GetString(memoryStream.ToArray());
        }
    }
}