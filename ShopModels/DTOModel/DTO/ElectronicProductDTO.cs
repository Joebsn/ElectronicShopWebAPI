namespace ElectronicShop.ShopModels.DTOModel.DTO
{
    public class ElectronicProductDTO
    {
        public int electronicproductID { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }

        public string? processor { get; set; }

        public int numberofcores { get; set; }

        public int screensize { get; set; }

        public int memory { get; set; }

        public int storage { get; set; }

        public int battery { get; set; }

        public int numberofproducts { get; set; }

        public int price { get; set; }

        public ElectronicProductDTO() { }

        public ElectronicProductDTO(string name, string type, string processor, int numberofcores, int screensize, int memory, int storage, int battery)
        {
            this.name = name;
            this.type = type;
            this.processor = processor;
            this.numberofcores = numberofcores;
            this.screensize = screensize;
            this.memory = memory;
            this.storage = storage;
            this.battery = battery;
        }
    }
}