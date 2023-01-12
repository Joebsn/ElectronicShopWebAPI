using System.ComponentModel.DataAnnotations;
using static ElectronicShop.ShopModels.DTOModel.CreatedDTO.CreatedElectronicProductDTO;

namespace ElectronicShop.ShopModels.DTOModel.GetDTO
{
    public class GetElectronicProductDTO
    {
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Maximum 15 characters, Minimum 3 characters")]
        public string? name { get; set; }

        [EnumDataType(typeof(AllElectronicProductsTypes))]
        public AllElectronicProductsTypes type { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Maximum 15 characters, Minimum 3 characters")]
        public string? processor { get; set; }
        [Range(0, 16)]
        public int numberofcores { get; set; }
        [Range(0, 20)]
        public int screensize { get; set; }
        [Range(0, 64)]
        public int memory { get; set; }
        [Range(0, 2000)]
        public int storage { get; set; }
        [Range(0, 35)]
        public int battery { get; set; }

        [Range(0, 100)]
        public int numberofproducts { get; set; }
        [Range(0, 3000)]
        public int price { get; set; }
    }
}