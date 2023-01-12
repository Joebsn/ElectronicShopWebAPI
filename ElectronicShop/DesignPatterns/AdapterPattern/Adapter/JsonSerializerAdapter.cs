using ElectronicShop.DesignPatterns.AdapterPattern.Target;
using System.Text.Json;

namespace ElectronicShop.DesignPatterns.AdapterPattern.Adapter
{
    public class JsonSerializerAdapter : ISerializerAdapter
    {
        public string Serialize<T>(object objToSerialize)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                return JsonSerializer.Serialize(objToSerialize, options);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}