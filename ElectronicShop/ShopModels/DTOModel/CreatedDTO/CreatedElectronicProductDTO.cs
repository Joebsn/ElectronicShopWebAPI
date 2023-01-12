using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ElectronicShop.ShopModels.DTOModel.CreatedDTO
{
    public class CreatedElectronicProductDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Maximum 15 characters, Minimum 3 characters")]
        public string? name { get; set; }

        [Required]
        [EnumDataType(typeof(AllElectronicProductsTypes))]
        public AllElectronicProductsTypes type { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Maximum 15 characters, Minimum 3 characters")]
        public string? processor { get; set; }
        [Required]
        [Range(2, 16)]
        public int numberofcores { get; set; }
        [Required]
        [Range(3, 20)]
        public int screensize { get; set; }
        [Required]
        [Range(2, 64)]
        public int memory { get; set; }
        [Required]
        [Range(8, 2000)]
        public int storage { get; set; }
        [Required]
        [Range(10, 35)]
        public int battery { get; set; }
        [Required]
        [Range(1, 100)]
        public int numberofproducts { get; set; }
        [Required]
        [Range(300, 3000)]
        public int price { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum AllElectronicProductsTypes
        {
            Phone,
            Laptop
        }
    }
}