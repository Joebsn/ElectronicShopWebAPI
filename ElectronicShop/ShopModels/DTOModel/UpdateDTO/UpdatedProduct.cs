using System.ComponentModel.DataAnnotations;
using static ElectronicShop.ShopModels.DTOModel.CreatedDTO.CreatedElectronicProductDTO;

namespace ElectronicShop.ShopModels.DTOModel.UpdateDTO
{
    public class UpdatedProduct
    {
        [Required]
        [Range(1, 1000000)]
        public int productid { get; set; }

        [Range(0, 100)]
        public int newnumberofproducts { get; set; }
        [Range(300, 3000)]
        public int newprice { get; set; } = 0;
    }
}