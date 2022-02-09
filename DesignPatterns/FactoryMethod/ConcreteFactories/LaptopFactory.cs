using ElectronicShop.DesignPatterns.FactoryMethod.ConcreteProducts;
using ElectronicShop.DesignPatterns.FactoryMethod.FactoryBase;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;

namespace ElectronicShop.DesignPatterns.FactoryMethod.ConcreteFactories
{
    public class LaptopFactory : IProductElectronicFactory
    {
        public LaptopFactory() { }
        public ProductElectronic CreateElectronicProduct(string name, string processor, int numberofcores, int screensize, int memory, 
                                                        int storage, int battery, int numberofproducts, int price)
        {
            return new Laptop(name, processor, numberofcores, screensize, memory, storage, battery, numberofproducts, price);
        }
    }
}