using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;

namespace ElectronicShop.DesignPatterns.FactoryMethod.ConcreteProducts
{
    public class Phone : ProductElectronic
    {
        public Phone(string name, string processor, int numberofcores, int screensize, int memory, int storage, int battery, int numberofproducts, int price)
        {
            this.Name = name;
            this.Type = GetName();
            this.Processor = processor;
            this.Numberofcores = numberofcores;
            this.Screensize = screensize;
            this.Memory = memory;
            this.Storage = storage;
            this.Battery = battery;
            this.Numberofproducts = numberofproducts;
            this.Price = price;
        }
        
        public override string ShowInfo()
        {
            return this.ToString();
        }

        public override string GetName()
        {
            return this.GetType().Name;
        }
    }
}