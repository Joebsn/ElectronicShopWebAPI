using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.ShopModels.DTOModel.GetDTO
{
    public class getadminDTO
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? password { get; set; }
    }
}
