using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.ShopModels.DTOModel.GetDTO
{
    public class getuserDTO
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? firstname { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? lastname { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? password { get; set; }
    }
}