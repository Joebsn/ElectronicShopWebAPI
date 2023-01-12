using ElectronicShop.DesignPatterns.FactoryMethod.ConcreteProducts;
using ElectronicShop.DesignPatterns.FactoryMethod.FactoryBase;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;

namespace ElectronicShop.DesignPatterns.FactoryMethod.ConcreteFactories
{
    public class PhoneFactory : IProductElectronicFactory
    {
        public PhoneFactory() { }
        public ProductElectronic CreateElectronicProduct(string name, string processor, int numberofcores, int screensize, int memory, 
                                                        int storage, int battery, int numberofproducts, int price)
        {
            return new Phone(name, processor, numberofcores, screensize, memory, storage, battery, numberofproducts, price);
        }
    }
}