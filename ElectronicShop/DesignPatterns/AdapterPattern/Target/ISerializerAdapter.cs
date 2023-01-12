namespace ElectronicShop.DesignPatterns.AdapterPattern.Target
{
    public interface ISerializerAdapter
    {
        string Serialize<T>(object objectToSerialize);
    }
}