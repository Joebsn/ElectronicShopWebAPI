using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShop.ShopModels.Models
{
    public class usermodel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? firstname { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? lastname { get; set; }

        [Column(TypeName = "int4")]
        public int age { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? password { get; set; }

        [Column(TypeName = "int4")]
        public int phonenumber { get; set; }

        [Column(TypeName = "float8")]
        public float balance { get; set; }
    }
}