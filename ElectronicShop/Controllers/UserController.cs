using ElectronicShop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ElectronicShop.Helper;
using ElectronicShop.Pagination;
using ElectronicShop.Controllers;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using Microsoft.AspNetCore.StaticFiles;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace ElectronicShop.Controllers
{
    [ApiController]

    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IUserShop _products;
        private readonly ILogger<ExceptionTableController> _logger;
        string s = "The function that throwed the exception is: ";
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public UserController(IUserShop products, ILogger<ExceptionTableController> logger, IMapper mapper, IUriService uriService)
        {
            _products = products;
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");
            _mapper = mapper;
            this.uriService = uriService;
        }


        [HttpGet("SearchAndFilter")]
        public async Task<ActionResult<APIResponse<List<ElectronicProductDTO>>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter, 
                                    bool buy = false, [Range(0, 100)] int quantitytobuy = 0, bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false)
        {
            try
            {
                var response = await _products.SearchAndFilter(product, filter, buy, quantitytobuy, CreateXML, CreateExcell, CreateJSON);

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

        [HttpGet("LogIn")]
        public async Task<ActionResult<APIResponse<userDTO>>> userlogin([FromQuery] getuserDTO getuser)
        {
            try
            {
                var response = await _products.userlogin(getuser);
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(userlogin));
                return NotFound(ex.Message);
            }
        }

        [HttpGet("LogOut")]
        public async Task<ActionResult<APIResponse<string>>> userlogout()
        {
            try
            {
                var response = await _products.userlogout();
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(userlogout));
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<APIResponse<userDTO>>> usersignup([FromQuery] CreatedUser createduser)
        {
            try
            {
                var response = await _products.usersignup(createduser);
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(usersignup));
                return NotFound(ex.Message);
            }
        }

        [HttpPut("ChangePasswordOrBalance")]
        public async Task<ActionResult<APIResponse<userDTO>>> userchangepasswordorBalance([FromQuery] UpdatedUserDTO updateduser)
        {
            try
            {
                var response = await _products.userchangepasswordorBalance(updateduser);
                if (response != null) return Ok(response);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(userchangepasswordorBalance));
                return NotFound(ex.Message);
            }
        }


        [HttpGet("SaveJSON,XML,Excellfiles")]
        public async Task<ActionResult> DownloadFile([EnumDataType(typeof(AllFilesTypes))] AllFilesTypes filetodownload)
        {
            try
            {
                string filename = "Products", filepath = "";
                if (filetodownload == AllFilesTypes.Excell) filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename.Trim() + ".xlsx");
                else if (filetodownload == AllFilesTypes.XML) filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename.Trim() + ".xml");
                else if (filetodownload == AllFilesTypes.JSON) filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename.Trim() + ".json");
                
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filepath, out var contentType)) contentType = "application/octet-stream";
                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(bytes, contentType, Path.GetFileName(filepath));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, s + nameof(DownloadFile));
                return NotFound(ex.Message);
            }
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum AllFilesTypes
        {
            Excell,
            XML,
            JSON
        }
        
    }
}