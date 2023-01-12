using ElectronicShop.Helper;
using ElectronicShop.Pagination;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DeletedDTO;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop.Interfaces
{
    public interface IAdminShop
    {
        Task<APIResponse<List<ElectronicProductDTO>>> GetAllElectronicProducts([FromQuery] PaginationFilter filter);
        Task<APIResponse<ElectronicProductDTO>> CreateElectronicProduct([FromQuery] CreatedElectronicProductDTO CreatedElectronicProduct);
        Task<APIResponse<List<string>>> GetAllTypes([FromQuery] PaginationFilter filter);
        Task<APIResponse<List<ElectronicProductDTO>>> DeleteProduct(DeletedElectronicProductDTO productToDelete, [FromQuery] PaginationFilter filter);
        Task<APIResponse<List<ElectronicProductDTO>>> UpdateProduct(UpdatedProduct productToUpdate, [FromQuery] PaginationFilter filter);

        Task<APIResponse<List<DisplayUserDetailDTO>>> GetUserDetail(int userID, [FromQuery] PaginationFilter filter);
        Task<APIResponse<userDTO>> adminlogin([FromQuery] getadminDTO getadmin);

        Task<APIResponse<string>> adminlogout();

        Task<APIResponse<List<ElectronicProductDTO>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter,
                                        bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false);
    }
}