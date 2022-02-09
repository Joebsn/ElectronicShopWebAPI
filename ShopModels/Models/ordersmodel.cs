using ElectronicShop.ShopModels.DTOModel.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShop.ShopModels.Models
{
    public class ordersmodel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderID { get; set; }

        [ForeignKey("usermodel")] //Defining the foreign key, we also initialize the class as we did below
        public int userID { get; set; }

        [Column(TypeName = "int4")]
        public int totalnumberofobjectsbought { get; set; }

        [Column(TypeName = "float8")]
        public float totalprice { get; set; }

        public usermodel? usermodel { get; set; }

        public ordersmodel() { }
        public ordersmodel(int userID, int totalnumberofobjectsbought, int totalprice)
        {
            this.userID = userID;
            this.totalnumberofobjectsbought = totalnumberofobjectsbought;
            this.totalprice = totalprice;
        }
    }
}