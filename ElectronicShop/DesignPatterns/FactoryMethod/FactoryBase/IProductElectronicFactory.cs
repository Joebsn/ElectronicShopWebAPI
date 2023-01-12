using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;

namespace ElectronicShop.DesignPatterns.FactoryMethod.FactoryBase
{
    public interface IProductElectronicFactory
    {
        ProductElectronic CreateElectronicProduct(string name, string processor, int numberofcores, int screensize, int memory, int storage, 
                                                    int battery, int numberofproducts, int price);
    }
}