using ElectronicShop.Helper;
using ElectronicShop.Pagination;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.Interfaces
{
    public interface IUserShop
    {
        Task<APIResponse<List<ElectronicProductDTO>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter, 
                                        bool buy = false, [Range(0, 100)] int quantitytobuy = 0, bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false);
        Task<APIResponse<userDTO>> userlogin([FromQuery] getuserDTO getuser);
        Task<APIResponse<userDTO>> usersignup([FromQuery] CreatedUser createduser);
        Task<APIResponse<userDTO>> userchangepasswordorBalance([FromQuery] UpdatedUserDTO updateduser);
        Task<APIResponse<string>> userlogout();
    }
}