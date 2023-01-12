using AutoMapper;
using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers;
using ElectronicShop.DesignPatterns.FactoryMethod.ConcreteFactories;
using ElectronicShop.DesignPatterns.FactoryMethod.FactoryBase;
using ElectronicShop.DesignPatterns.FactoryMethod.ProductBase;
using ElectronicShop.Helper;
using ElectronicShop.Interfaces;
using ElectronicShop.Pagination;
using ElectronicShop.ShopModels;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DeletedDTO;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using ElectronicShop.ShopModels.Models;
using ElectronicShop.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using ElectronicShop.DesignPatterns.AdapterPattern.Adapter;
using ElectronicShop.DesignPatterns.AdapterPattern;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ElectronicShop.Repository
{
    public class ShopRepository : IAdminShop
    {
        private readonly ShopDbContext _context;

        string s = "The function that throwed the exception is: ";

        private readonly ILogger<ExceptionTableController> _logger;
        private readonly IMapper _mapper;
        private bool isLoggedIn = false;
        List<ElectronicProductDTO> allproducts = new List<ElectronicProductDTO>();

        public ShopRepository(ShopDbContext context, ILogger<ExceptionTableController> logger, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");

            isLoggedIn = false;
            allproducts = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();
        }

        public async Task<APIResponse<List<ElectronicProductDTO>>> GetAllElectronicProducts([FromQuery] PaginationFilter filter)
        {

            APIResponse<List<ElectronicProductDTO>> ApiResponse = new APIResponse<List<ElectronicProductDTO>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }

            try
            {
                ApiResponse.Data = new List<ElectronicProductDTO>();
                ApiResponse.Data = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();

                if (ApiResponse.Data != null)
                {
                    ApiResponse.CheckGetAll(ApiResponse.Data.Count);

                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(GetAllElectronicProducts));
                return ApiResponse;
            }
        }

        public async Task<APIResponse<ElectronicProductDTO>> CreateElectronicProduct([FromQuery] CreatedElectronicProductDTO CreatedElectronicProduct)
        {
            APIResponse<ElectronicProductDTO> ApiResponse = new APIResponse<ElectronicProductDTO>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }

            try
            {
                ApiResponse.Data = new ElectronicProductDTO();
                ApiResponse.Data = _mapper.Map<ElectronicProductDTO>(CreatedElectronicProduct);

                if (ApiResponse.Data == null)
                {
                    ApiResponse.Message = "Object was not created";
                    return ApiResponse;
                }

                string producttype = CreatedElectronicProduct.type.ToString();
                AddElectronicProductResult isaccepted = AddElectronicProductResult.Rejected;
                IProductElectronicFactory factory;

                var batteryHandler = new ProductBatteryHandler(); var memoryHandler = new ProductMemoryHandler();
                var numberofcoresHandler = new ProductNumberOfCoresHandler(); var priceHandler = new ProductPriceHandler();
                var screensizeHandler = new ProductScreenSizeHandler(); var storageHandler = new ProductStorageHandler();
                batteryHandler.SetSuccessor(memoryHandler); memoryHandler.SetSuccessor(numberofcoresHandler);
                numberofcoresHandler.SetSuccessor(priceHandler); priceHandler.SetSuccessor(screensizeHandler);
                screensizeHandler.SetSuccessor(storageHandler);

                if (producttype == "Phone")
                {
                    isaccepted = batteryHandler.EvaluatePhoneSpecifications(ApiResponse.Data);
                    factory = new PhoneFactory();
                }

                else // producttype == "Laptop"
                {
                    isaccepted = batteryHandler.EvaluateLaptopSpecifications(ApiResponse.Data);
                    factory = new LaptopFactory();
                }

                if (isaccepted == AddElectronicProductResult.Accepted)
                {
                    var producttoadd = (from c in _context.electronicproducts
                                        where c.name != null && c.name.Trim().ToLower() == ApiResponse.Data.name!.Trim().ToLower()
                                      && c.type != null && c.type.Trim().ToLower() == ApiResponse.Data.type!.Trim().ToLower()
                                      && c.processor!.Trim().ToLower() == ApiResponse.Data.processor!.Trim().ToLower() && c.numberofcores == ApiResponse.Data.numberofcores
                                      && c.screensize == ApiResponse.Data.screensize && c.memory == ApiResponse.Data.memory && c.storage == ApiResponse.Data.storage
                                      && c.battery == ApiResponse.Data.battery && c.price == ApiResponse.Data.price
                                        select c).FirstOrDefault();

                    if (producttoadd != null) //so it already exists in the database, we only add the numberof products
                    {
                        producttoadd.numberofproducts = producttoadd.numberofproducts + CreatedElectronicProduct.numberofproducts;
                        ApiResponse.Data.numberofproducts = producttoadd.numberofproducts;
                        ApiResponse.Data.electronicproductID = producttoadd.electronicproductID;
                        _context.Update(producttoadd);
                        await _context.SaveChangesAsync();
                    }
                    else //add it to the database if it doesn't exist
                    {
                        ProductElectronic a = factory.CreateElectronicProduct(CreatedElectronicProduct.name!, CreatedElectronicProduct.processor!,
                        CreatedElectronicProduct.numberofcores, CreatedElectronicProduct.screensize, CreatedElectronicProduct.memory,
                        CreatedElectronicProduct.storage, CreatedElectronicProduct.battery, CreatedElectronicProduct.numberofproducts, CreatedElectronicProduct.price);


                        electronicproductmodel p = new electronicproductmodel(a.Name!, a.Type!, a.Processor, a.Numberofcores, a.Screensize, a.Memory, a.Storage, a.Battery,
                            a.Numberofproducts, a.Price);
                        /*
                        electronicproductmodel p = new electronicproductmodel();
                        p.name = a.Name;                            p.type = a.Type;                    p.processor = a.Processor;
                        p.numberofcores = a.Numberofcores;          p.screensize = a.Screensize;        p.memory = a.Memory;
                        p.storage = a.Storage;                      p.battery = a.Battery;              p.numberofproducts = a.Numberofproducts;
                        p.price = a.Price;
                        */


                        if (_context.electronicproducts != null) _context.electronicproducts.Add(p);
                        await _context.SaveChangesAsync();
                        ApiResponse.Data.electronicproductID = (from c in _context.electronicproducts
                                                                where c.name != null && c.name.Trim().ToLower() == ApiResponse.Data.name!.Trim().ToLower()
                                                                && c.type != null && c.type.Trim().ToLower() == ApiResponse.Data.type!.Trim().ToLower()
                                                                && c.processor!.Trim().ToLower() == ApiResponse.Data.processor!.Trim().ToLower() && c.numberofcores == ApiResponse.Data.numberofcores
                                                                && c.screensize == ApiResponse.Data.screensize && c.memory == ApiResponse.Data.memory && c.storage == ApiResponse.Data.storage
                                                                && c.battery == ApiResponse.Data.battery && c.price == ApiResponse.Data.price
                                                                select c.electronicproductID).FirstOrDefault();
                    }
                }
                else
                {
                    ApiResponse.Message = "Object was rejected so it wasn't created";
                    return ApiResponse;
                }

                if (ApiResponse.Data != null)
                {
                    ApiResponse.Message = "Object created successfully";
                    ApiResponse.SuccessTrue();
                }
                else ApiResponse.NoData();
                return ApiResponse;

            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(CreateElectronicProduct));
                return ApiResponse;
            }
        }

        public async Task<APIResponse<List<string>>> GetAllTypes([FromQuery] PaginationFilter filter)
        {
            APIResponse<List<string>> ApiResponse = new APIResponse<List<string>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }

            try
            {
                ApiResponse.Data = new List<string>();
                var types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (var type in types)
                {
                    var a = type.BaseType;
                    if (type.BaseType == typeof(ProductElectronic))
                    {
                        ApiResponse.Data.Add(type.Name);
                    }
                }
                if (ApiResponse.Data != null)
                {
                    ApiResponse.CheckGetAll(ApiResponse.Data.Count);
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(GetAllTypes));
                return ApiResponse;
            }
        }

        public async Task<APIResponse<List<ElectronicProductDTO>>> DeleteProduct(DeletedElectronicProductDTO productToDelete, [FromQuery] PaginationFilter filter)
        {
            APIResponse<List<ElectronicProductDTO>> ApiResponse = new APIResponse<List<ElectronicProductDTO>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }

            try
            {
                ApiResponse.Data = new List<ElectronicProductDTO>();

                electronicproductmodel? todelete = (from c in _context.electronicproducts where c.electronicproductID! == productToDelete.productid! select c).FirstOrDefault();
                if (todelete != null)
                {
                    _context.electronicproducts!.Remove(todelete);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ApiResponse.Message = "No Product has this id";
                }

                ApiResponse.Data = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();

                if (ApiResponse.Data != null)
                {
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(DeleteProduct));
                return ApiResponse;
            }
        }

        public async Task<APIResponse<List<ElectronicProductDTO>>> UpdateProduct(UpdatedProduct productToUpdate, [FromQuery] PaginationFilter filter)
        {
            APIResponse<List<ElectronicProductDTO>> ApiResponse = new APIResponse<List<ElectronicProductDTO>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }

            try
            {
                ApiResponse.Data = new List<ElectronicProductDTO>();

                electronicproductmodel? toupdate = (from c in _context.electronicproducts where c.electronicproductID! == productToUpdate.productid! select c).FirstOrDefault();

                if (toupdate != null) //so it exists in the database
                {
                    toupdate.numberofproducts = productToUpdate.newnumberofproducts > 0 ? productToUpdate.newnumberofproducts : toupdate.numberofproducts;
                    toupdate.price = productToUpdate.newprice > 0 ? productToUpdate.newprice : toupdate.price;

                    _context.Update(toupdate);
                    await _context.SaveChangesAsync();

                }
                else //It is not in the database
                {
                    ApiResponse.Message = "This product is not in the database";
                    return ApiResponse;
                }

                ApiResponse.Data = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();
                if (ApiResponse.Data != null)
                {
                    ApiResponse.CheckGetAll(ApiResponse.Data.Count);

                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(DeleteProduct));
                return ApiResponse;
            }
        }

        public async Task<APIResponse<List<DisplayUserDetailDTO>>> GetUserDetail(int userID, [FromQuery] PaginationFilter filter)
        {
            APIResponse<List<DisplayUserDetailDTO>> ApiResponse = new APIResponse<List<DisplayUserDetailDTO>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }
            try
            {
                ApiResponse.Data = new List<DisplayUserDetailDTO>();

                List<usermodel> users = _context.users!.ToList();
                List<ordersmodel> orders = _context.orders!.ToList();
                List<orderdetailsmodel> ordersdetails = _context.ordersdetails!.ToList();
                List<electronicproductmodel> products = _context.electronicproducts!.ToList();

                var userrecord = from u in users
                                 where u.userID == userID
                                 join o in orders on u.userID equals o.userID into table1
                                 from o in table1.ToList()
                                 join d in ordersdetails on o.orderID equals d.orderID into table2
                                 from d in table2.ToList()
                                 join e in products on d.electronicproductID equals e.electronicproductID into table3
                                 from e in table3.ToList()
                                 select new DisplayUserDetailDTO
                                 {
                                     Specificuser = _mapper.Map<userDTO>(u),
                                     Allorders = _mapper.Map<ordersDTO>(o),
                                     AllOrderDetails = _mapper.Map<orderdetailsDTO>(d),
                                     electronicproductobject = _mapper.Map<ElectronicProductDTO>(e)
                                 };
                ApiResponse.Data = userrecord.ToList();

                if (ApiResponse.Data != null)
                {
                    ApiResponse.CheckGetAll(ApiResponse.Data.Count);

                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(DeleteProduct));
                return ApiResponse;
            }
        }


        public async Task<APIResponse<userDTO>> adminlogin([FromQuery] getadminDTO getadmin)
        {
            APIResponse<userDTO> userresponse = new APIResponse<userDTO>();
            try
            {
                if (getadmin.name == "admin" && getadmin.password == "admin")
                {
                    isLoggedIn = true;
                    userresponse.Message = "Admin logged in";
                    userresponse.SuccessTrue();
                }
                else userresponse.Message = "Wrong informations";
                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(adminlogin));
                return userresponse;
            }
        }

        public async Task<APIResponse<string>> adminlogout()
        {
            APIResponse<string> userresponse = new APIResponse<string>();
            try
            {
                if (isLoggedIn)
                {
                    isLoggedIn = false;
                    userresponse.Message = "You are logged out";
                }
                else
                    userresponse.Message = "You have to login first";
                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(adminlogin));
                return userresponse;
            }
        }


        public async Task<APIResponse<List<ElectronicProductDTO>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter,
                                        bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false)
        {
            APIResponse<List<ElectronicProductDTO>> ApiResponse = new APIResponse<List<ElectronicProductDTO>>();
            if (isLoggedIn == false)
            {
                ApiResponse.Message = "Log In To your admin account first";
                return ApiResponse;
            }
            try
            {
                allproducts = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();
                ApiResponse.Data = allproducts;

                if (ApiResponse.Data != null)
                {
                    if ((product.name != null) || (product.type.ToString() != null) || (product.processor != null) || (product.numberofcores != 0) || (product.screensize != 0) ||
                        (product.memory != 0) || (product.storage != 0) || (product.battery != 0) || (product.numberofproducts != 0) || (product.price != 0))
                    {
                        if (product.name != null)
                        {
                            product.name = Regex.Replace(product.name.Trim(), @"\s+", " ");
                            ApiResponse.Data = (from p in ApiResponse.Data where p.name!.ToLower().Contains(product.name.Trim().ToLower()) select p).ToList();
                        }

                        if (product.type.ToString() != null)
                        {
                            string producttype = Regex.Replace(product.type.ToString().Trim(), @"\s+", " ");
                            ApiResponse.Data = (from p in ApiResponse.Data where p.type!.ToLower() == producttype.Trim().ToLower() select p).ToList();
                        }

                        if (product.processor != null)
                        {
                            product.processor = Regex.Replace(product.processor.Trim(), @"\s+", " ");
                            ApiResponse.Data = (from p in ApiResponse.Data where p.processor!.ToLower().Contains(product.processor.Trim().ToLower()) select p).ToList();
                        }

                        if (product.numberofcores != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.numberofcores == product.numberofcores select p).ToList();

                        if (product.screensize != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.screensize == product.screensize select p).ToList();

                        if (product.memory != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.memory == product.memory select p).ToList();

                        if (product.storage != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.storage == product.storage select p).ToList();

                        if (product.battery != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.battery == product.battery select p).ToList();

                        if (product.numberofproducts != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.numberofproducts == product.numberofproducts select p).ToList();

                        if (product.price != 0) ApiResponse.Data = (from p in ApiResponse.Data where p.price == product.price select p).ToList();
                    }
                }

                if (ApiResponse.Data != null)
                {
                    if (CreateXML)
                    {
                        var serializer = new DataSerializer(new XMLSerializerAdapter());
                        serializer.CreateXMLFileOfElectronicProducts(ApiResponse.Data);
                    }

                    if (CreateJSON)
                    {
                        var serializer = new DataSerializer(new JsonSerializerAdapter());
                        serializer.CreateJSONFileOfElectronicProducts(ApiResponse.Data);
                    }

                    if (CreateExcell)
                    {
                        var serializer = new DataSerializer(new ExcellSerializerAdapter());
                        serializer.CreateExcellFileOfElectronicProducts(ApiResponse.Data);
                    }

                    ApiResponse.CheckGetAll(ApiResponse.Data.Count);
                }

                if (ApiResponse.Data != null)
                {
                    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                    var pagedData = ApiResponse.Data.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
                    var totalRecords = ApiResponse.Data.Count();
                    ApiResponse.Data = pagedData;
                    ApiResponse.TotalCounts = totalRecords;
                }
                else ApiResponse.NoData();

                return ApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(SearchAndFilter));
                return ApiResponse;
            }
        }
    }
}