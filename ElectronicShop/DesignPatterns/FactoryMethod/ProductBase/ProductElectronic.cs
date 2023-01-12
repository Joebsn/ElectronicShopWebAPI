namespace ElectronicShop.DesignPatterns.FactoryMethod.ProductBase
{
    public abstract class ProductElectronic
    {
        private string? name { get; set; }
        private string? type { get; set; }
        private string processor { get; set; } = default!;
        private int numberofcores { get; set; }
        private int screensize { get; set; }
        private int memory { get; set; }
        private int storage { get; set; }
        private int battery { get; set; }
        private int numberofproducts { get; set; }
        private int price { get; set; }

        public abstract string ShowInfo();

        public abstract string GetName();

        public override string ToString()
        {
            return "Name: " + Name + "\nType " + Type + "\nProcessor " + Processor + "\nNumber Of Cores " + Numberofcores + "\nScreensize " + 
                    Screensize + " inch\nMemory " + Memory + " GB\nStorage " + Storage + " GB\nBattery " + Battery + " hours\nNumber Of Products" 
                    + Numberofproducts + "\nPrice: " + Price;
        }

        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        public string? Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Processor
        {
            get { return processor; }
            set { processor = value; }
        }

        public int Numberofcores
        {
            get { return numberofcores; }
            set { numberofcores = value; }
        }

        public int Screensize
        {
            get { return screensize; }
            set { screensize = value; }
        }

        public int Memory
        {
            get { return memory; }
            set { memory = value; }
        }

        public int Storage
        {
            get { return storage; }
            set { storage = value; }
        }

        public int Battery
        {
            get { return battery; }
            set { battery = value; }
        }

        public int Numberofproducts
        {
            get { return numberofproducts; }
            set { numberofproducts = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}