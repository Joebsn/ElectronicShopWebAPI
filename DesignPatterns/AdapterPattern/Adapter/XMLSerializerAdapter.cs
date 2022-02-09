using ElectronicShop.DesignPatterns.AdapterPattern.Target;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ElectronicShop.DesignPatterns.AdapterPattern.Adapter
{
    public class XMLSerializerAdapter : ISerializerAdapter
    {
        public string Serialize<T>(object objToSerialize)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, objToSerialize);
                    return writer.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}