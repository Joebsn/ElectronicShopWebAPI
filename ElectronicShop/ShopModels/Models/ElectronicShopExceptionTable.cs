using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShop.ShopModels.Models
{
    public class ElectronicShopExceptionTable
    {
        [Key] //This is the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(500)")] //Creating the column in the database as varchar(500)
        public string? Message { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Level { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Logger { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Application { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Callsite { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Exception { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Logged { get; set; }
    }
}
