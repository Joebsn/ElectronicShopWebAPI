using AutoMapper;
using ElectronicShop.Helper;
using ElectronicShop.Interfaces;
using ElectronicShop.Pagination;
using ElectronicShop.Controllers;
using Microsoft.AspNetCore.Mvc;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DeletedDTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;

namespace ElectronicShop.Controllers
{
    [ApiController]

    [Route("Admin")]
    public class ShopController : ControllerBase
    {
        private readonly IAdminShop _products;
        private readonly ILogger<ExceptionTableController> _logger;
        string s = "The function that throwed the exception is: ";
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public ShopController(IAdminShop products, ILogger<ExceptionTableController> logger, IMapper mapper, IUriService uriService)
        {
            _products = products;
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");
            _mapper = mapper;
            this.uriService = uriService;
        }


        [HttpGet("GetAllElectronicProducts")]
        public async Task<ActionResult<APIResponse<List<ElectronicProductDTO>>>> GetAllElectronicProducts([FromQuery] PaginationFilter filter)
        {
            try
            {
                var response = await _products.GetAllElectronicProducts(filter);

                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<ElectronicProductDTO>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(GetAllElectronicProducts));
                return NotFound(ex.Message);
            }
        }

        [HttpGet("LogIn")]
        public async Task<ActionResult<APIResponse<userDTO>>> adminlogin([FromQuery] getadminDTO getadmin)
        {
            try
            {
                var response = await _products.adminlogin(getadmin);
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(adminlogin));
                return NotFound(ex.Message);
            }
        }

        [HttpGet("LogOut")]
        public async Task<ActionResult<APIResponse<string>>> adminlogout()
        {
            try
            {
                var response = await _products.adminlogout();
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(adminlogout));
                return NotFound(ex.Message);
            }
        }


        [HttpPost("CreateElectronicProduct")]
        public async Task<ActionResult<APIResponse<ElectronicProductDTO>>> CreateElectronicProduct([FromQuery] CreatedElectronicProductDTO createdphone)
        {
            try
            {
                var response = await _products.CreateElectronicProduct(createdphone);
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(CreateElectronicProduct));
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<APIResponse<List<string>>>> GetAllTypes([FromQuery] PaginationFilter filter)
        {
            try
            {
                var response = await _products.GetAllTypes(filter);

                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<string>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(GetAllTypes));
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<APIResponse<List<ElectronicProductDTO>>>> DeleteProduct(DeletedElectronicProductDTO productToDelete, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var response = await _products.DeleteProduct(productToDelete, filter);

                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<ElectronicProductDTO>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(DeleteProduct));
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<APIResponse<List<ElectronicProductDTO>>>> UpdateProduct(UpdatedProduct productToUpdate, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var response = await _products.UpdateProduct(productToUpdate, filter);

                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<ElectronicProductDTO>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(CreateElectronicProduct));
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetUserDetail")]
        public async Task<ActionResult<APIResponse<List<DisplayUserDetailDTO>>>> GetUserDetail(int userID, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var response = await _products.GetUserDetail(userID, filter);
                
                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<DisplayUserDetailDTO>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NotFound(response);
                
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex, s + nameof(GetUserDetail));
                return NotFound(ex.Message);
            }
        }

        [HttpGet("SearchAndFilter")]
        public async Task<ActionResult<APIResponse<List<ElectronicProductDTO>>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter,
                                    bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false)
        {
            try
            {
                var response = await _products.SearchAndFilter(product, filter, CreateXML, CreateExcell, CreateJSON);

                if (response != null)
                {
                    var route = Request.Path.Value;
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var totalRecords = response.TotalCounts;
                    if (response.Data == null) return NotFound(response);
                    var pagedReponse = PaginationHelper.CreatePagedReponse<ElectronicProductDTO>(response.Data, validFilter, totalRecords, uriService, route!);
                    pagedReponse.Message = response.Message;
                    return Ok(pagedReponse);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(SearchAndFilter));
                return NotFound(ex.Message);
            }
        }
    }
}