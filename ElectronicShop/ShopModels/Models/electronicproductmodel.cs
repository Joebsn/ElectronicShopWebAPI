using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShop.ShopModels.Models
{
    public class electronicproductmodel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int electronicproductID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? type { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? processor { get; set; } //phone or laptop

        [Column(TypeName = "int4")]
        public int numberofcores { get; set; }

        [Column(TypeName = "int4")]
        public int screensize { get; set; }

        [Column(TypeName = "int4")]
        public int memory { get; set; }

        [Column(TypeName = "int4")]
        public int storage { get; set; }

        [Column(TypeName = "int4")]
        public int battery { get; set; }

        [Column(TypeName = "int4")]
        public int numberofproducts { get; set; }

        [Column(TypeName = "int4")]
        public int price { get; set; }

        public electronicproductmodel() { }

        public electronicproductmodel(int electronicproductid, string name, string type, string processor, int numberofcores, int screensize, int memory, int storage, 
                                    int battery, int numberofproducts, int price)
        {
            this.electronicproductID = electronicproductid;     this.name = name;                           this.type = type;           
            this.processor = processor;                         this.numberofcores = numberofcores;         this.screensize = screensize;               
            this.memory = memory;                               this.storage = storage;                     this.battery = battery;                        
            this.numberofproducts = numberofproducts;           this.price = price;
        }

        public electronicproductmodel(string name, string type, string processor, int numberofcores, int screensize, int memory, int storage,
                                    int battery, int numberofproducts, int price)
        {
            this.name = name; this.type = type;
            this.processor = processor; this.numberofcores = numberofcores; this.screensize = screensize;
            this.memory = memory; this.storage = storage; this.battery = battery;
            this.numberofproducts = numberofproducts; this.price = price;
        }
    }
}