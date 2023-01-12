using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.ShopModels.DTOModel.UpdateDTO
{
    public class UpdatedUserDTO
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? firstname { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? lastname { get; set; }
        [Required]
        [Range(70000000, 80000000)]
        public int phonenumber { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? password { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? newpassword { get; set; }

        [Range(0, 1000000)]
        public float newbalance { get; set; }

    }
}