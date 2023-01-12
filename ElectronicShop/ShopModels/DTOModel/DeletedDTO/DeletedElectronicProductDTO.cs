using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static ElectronicShop.ShopModels.DTOModel.CreatedDTO.CreatedElectronicProductDTO;

namespace ElectronicShop.ShopModels.DTOModel.DeletedDTO
{
    public class DeletedElectronicProductDTO
    {
        [Required]
        [Range(1, 1000000)]
        public int productid { get; set; }
    }
}