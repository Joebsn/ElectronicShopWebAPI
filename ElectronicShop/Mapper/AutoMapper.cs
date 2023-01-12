using AutoMapper;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DeletedDTO;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using ElectronicShop.ShopModels.Models;

namespace ElectronicShop.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<electronicproductmodel, ElectronicProductDTO>();
            CreateMap<DeletedElectronicProductDTO, electronicproductmodel>();
            CreateMap<ElectronicProductDTO, electronicproductmodel>();
            CreateMap<CreatedElectronicProductDTO, ElectronicProductDTO>();

            CreateMap<usermodel, userDTO>();
            CreateMap<CreatedUser, usermodel>();

            CreateMap<ordersmodel, ordersDTO>();
            CreateMap<orderdetailsmodel, orderdetailsDTO>();

            CreateMap<usermodel, DisplayUserDetailDTO>();
        }
    }
}