using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.ShopModels.DTOModel.CreatedDTO
{
    public class CreatedUser
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? firstname { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? lastname { get; set; }
        [Required]
        [Range(18, 100)]
        public int age { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Maximum 10 characters, Minimum 3 characters")]
        public string? password { get; set; }
        [Required]
        [Range(70000000, 80000000)]
        public int phonenumber { get; set; }
        [Required]
        [Range(0, 1000000)]
        public float balance { get; set; }
    }
}