using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShop.ShopModels.Models
{
    public class orderdetailsmodel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderdetailID{ get; set; }

        [ForeignKey("ordersmodel")] //Defining the foreign key, we also initialize the class as we did below
        public int orderID { get; set; }

        [ForeignKey("electronicproductmodel")] //Defining the foreign key, we also initialize the class as we did below
        public int electronicproductID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime boughtdate { get; set; }

        [Column(TypeName = "int4")]
        public int quantity { get; set; }

        [Column(TypeName = "float8")]
        public float price { get; set; }

        public ordersmodel? ordersmodel { get; set; }

        public electronicproductmodel? electronicproduct { get; set; }
    }
}