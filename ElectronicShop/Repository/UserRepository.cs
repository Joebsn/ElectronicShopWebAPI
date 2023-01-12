using AutoMapper;
using ElectronicShop.DesignPatterns.AdapterPattern;
using ElectronicShop.DesignPatterns.AdapterPattern.Adapter;
using ElectronicShop.Helper;
using ElectronicShop.Interfaces;
using ElectronicShop.Pagination;
using ElectronicShop.ShopModels;
using ElectronicShop.ShopModels.DTOModel.CreatedDTO;
using ElectronicShop.ShopModels.DTOModel.DTO;
using ElectronicShop.ShopModels.DTOModel.GetDTO;
using ElectronicShop.ShopModels.DTOModel.UpdateDTO;
using ElectronicShop.ShopModels.Models;
using ElectronicShop.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ElectronicShop.Repository
{
    public class UserRepository : IUserShop
    {
        private readonly ShopDbContext _context;

        string s = "The function that throwed the exception is: ";

        private readonly ILogger<ExceptionTableController> _logger;
        private readonly IMapper _mapper;

        public static bool firstbuy;
        public int currentorderid;
        public userDTO? currentuser;

        List<ElectronicProductDTO> allproducts = new List<ElectronicProductDTO>();
        public UserRepository(ShopDbContext context, ILogger<ExceptionTableController> logger, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");

            firstbuy = true;
            currentorderid = 0;
            allproducts = (from c in _context.electronicproducts select _mapper.Map<ElectronicProductDTO>(c)).ToList();
        }

        public async Task<APIResponse<List<ElectronicProductDTO>>> SearchAndFilter([FromQuery] GetElectronicProductDTO product, [FromQuery] PaginationFilter filter,
                                        bool buy = false, [Range(0, 100)] int quantitytobuy = 0, bool CreateXML = false, bool CreateExcell = false, bool CreateJSON = false)
        {
            APIResponse<List<ElectronicProductDTO>> ApiResponse = new APIResponse<List<ElectronicProductDTO>>();

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

                if (ApiResponse.Data != null && ApiResponse.Data.Count == 1 && buy == true && quantitytobuy > 0)
                {
                    ElectronicProductDTO producttobuy = ApiResponse.Data[0];

                    int price = producttobuy.price * quantitytobuy;
                    if (currentuser != null && currentuser.userID != 0)
                    {
                        if (quantitytobuy <= producttobuy.numberofproducts && quantitytobuy != 0)
                        {
                            if (currentuser.balance >= price)
                            {
                                if (firstbuy == true)
                                {
                                    ordersmodel order = new ordersmodel(currentuser.userID, quantitytobuy, price);

                                    _context.orders!.Add(order);
                                    await _context.SaveChangesAsync();

                                    //User may have bought many times so we have to get the lastorderID which is the currentorderID
                                    List<int> AllCurrentUserOrderID = (from c in _context.orders
                                                                       where c.userID == currentuser.userID
                                                                        && c.totalnumberofobjectsbought == quantitytobuy && c.totalprice == price
                                                                       select c.orderID).ToList();

                                    currentorderid = AllCurrentUserOrderID.Last();
                                    firstbuy = false;
                                }
                                else
                                {
                                    //Update the total price and the totalnumberofobjectbought in the order table using the currentorderid
                                    ordersmodel? order = new ordersmodel();
                                    order = (from c in _context.orders where c.orderID == currentorderid && c.userID == currentuser.userID select c).FirstOrDefault();
                                    if (order != null)
                                    {
                                        order.totalnumberofobjectsbought = order.totalnumberofobjectsbought + quantitytobuy;
                                        order.totalprice = order.totalprice + price;
                                        _context.orders!.Update(order);
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        ApiResponse.Message = "Order wasn't saved correctly";
                                        return ApiResponse;
                                    }
                                }

                                //update the orderdetailtable
                                orderdetailsmodel orderdetails = new orderdetailsmodel();

                                orderdetails.quantity = quantitytobuy; orderdetails.price = price;
                                orderdetails.boughtdate = DateTime.Now; orderdetails.orderID = currentorderid;
                                orderdetails.electronicproductID = producttobuy.electronicproductID;

                                _context.ordersdetails!.Add(orderdetails);
                                await _context.SaveChangesAsync();

                                //remove the quantity bought from the product table

                                electronicproductmodel? a = new electronicproductmodel();
                                a = (from c in _context.electronicproducts where c.electronicproductID == producttobuy.electronicproductID select c).FirstOrDefault();
                                if (a != null)
                                {
                                    a.numberofproducts = a.numberofproducts - quantitytobuy;
                                    ApiResponse.Data[0].numberofproducts = a.numberofproducts;
                                    _context.electronicproducts!.Update(a);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    ApiResponse.Message = "Number of products wasn't changed correctly";
                                    return ApiResponse;
                                }

                                //remove the money from the user table
                                usermodel? user = new usermodel();
                                user = (from c in _context.users where c.userID == currentuser.userID select c).FirstOrDefault();
                                if (user != null)
                                {
                                    user.balance = user.balance - price;
                                    _context.users!.Update(user);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    ApiResponse.Message = "Balance wasn't changed correctly";
                                    return ApiResponse;
                                }
                                await _context.SaveChangesAsync();

                                //int currentuserid = currentuser.userID;
                                //currentuser = (from c in _context.users where c.userID == currentuserid select _mapper.Map<userDTO>(c)).FirstOrDefault();

                            }
                            else ApiResponse.Message = "You don't have enough money";
                        }
                        else ApiResponse.Message = "Enter a positive quantity such that there is enough of it in the store";
                    }
                    else ApiResponse.Message = "Log In To your account first";
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

        public async Task<APIResponse<userDTO>> userlogin([FromQuery] getuserDTO getuser)
        {
            APIResponse<userDTO> userresponse = new APIResponse<userDTO>();
            try
            {
                userresponse.Data = (from c in _context.users
                                     where (c.firstname!.ToLower() == getuser.firstname!.ToLower() && c.lastname!.ToLower() == getuser.lastname!.ToLower()
                                        && c.password!.ToLower() == getuser.password!.ToLower())
                                     select _mapper.Map<userDTO>(c)).FirstOrDefault()!;
                if (userresponse.Data != null)
                {
                    firstbuy = true;
                    currentuser = userresponse.Data;
                    currentuser.userID = userresponse.Data.userID;
                    userresponse.SuccessTrue();
                }
                else userresponse.Message = "Wrong informations";
                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(userlogin));
                return userresponse;
            }
        }

        public async Task<APIResponse<userDTO>> usersignup([FromQuery] CreatedUser createduser)
        {
            APIResponse<userDTO> userresponse = new APIResponse<userDTO>();
            try
            {
                string[] numbers = { "1", "4", "6", "9" };
                string number = "7", firstnumber = createduser.phonenumber.ToString().Substring(0, 1), secondnumber = createduser.phonenumber.ToString().Substring(1, 1), s = "";
                bool acceptablenumber = false;
                if (firstnumber == number)
                {
                    foreach (string a in numbers)
                    {
                        s += a + ", ";
                        if (secondnumber == a) acceptablenumber = true;
                    }
                    if (!acceptablenumber)
                    {
                        userresponse.Message = "Second number should be one of these: " + s;
                        return userresponse;
                    }
                }
                else
                {
                    userresponse.Message = "Phone number should start with 7";
                    return userresponse;
                }

                //check if user already have an account
                userresponse.Data = (from c in _context.users
                                     where (c.firstname!.ToLower() == createduser.firstname!.ToLower() && c.lastname!.ToLower() == createduser.lastname!.ToLower()
                                        && c.age == createduser.age && c.phonenumber == createduser.phonenumber)
                                     select _mapper.Map<userDTO>(c)).FirstOrDefault()!;

                if (userresponse.Data == null)
                {
                    var a = _mapper.Map<usermodel>(createduser);
                    _context.users!.Add(a);
                    await _context.SaveChangesAsync();

                    userresponse.Data = _mapper.Map<userDTO>(a);
                    if (userresponse.Data != null)
                    {
                        currentuser = userresponse.Data;
                        firstbuy = true;
                    }
                    userresponse.Message = "User added";
                }
                else userresponse.Message = "User already have an account";

                userresponse.SuccessTrue();
                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(usersignup));
                return userresponse;
            }
        }

        public async Task<APIResponse<userDTO>> userchangepasswordorBalance([FromQuery] UpdatedUserDTO updateduser)
        {
            APIResponse<userDTO> userresponse = new APIResponse<userDTO>();
            try
            {
                usermodel u = (from c in _context.users
                               where (c.firstname!.ToLower() == updateduser.firstname!.ToLower() && c.lastname!.ToLower() == updateduser.lastname!.ToLower()
                                 && c.password!.ToLower() == updateduser.password!.ToLower() && c.phonenumber == updateduser.phonenumber)
                               select c).FirstOrDefault()!;

                if (u != null)
                {
                    if (updateduser.newpassword != null) u.password = updateduser.newpassword!;
                    if (updateduser.newbalance != 0) u.balance = updateduser.newbalance!;
                    _context.users!.Update(u);
                    await _context.SaveChangesAsync();
                    userresponse.Message = "User updated";
                    userresponse.Data = _mapper.Map<userDTO>(u);
                    if (userresponse.Data != null) currentuser = userresponse.Data;
                    userresponse.SuccessTrue();
                }
                else
                {
                    userresponse.Message = "User doesn't exists";
                    userresponse.NotExist();
                }

                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(userchangepasswordorBalance));
                return userresponse;
            }
        }

        public async Task<APIResponse<string>> userlogout()
        {
            APIResponse<string> userresponse = new APIResponse<string>();
            try
            {
                if (currentuser != null && currentuser.userID != 0)
                {
                    currentuser = null;
                    userresponse.Message = "You are logged out";
                }
                else
                    userresponse.Message = "You have to login first";
                return userresponse;
            }
            catch (Exception ex)
            {
                userresponse.OperationFailed(ex);
                _logger.LogError(ex, s + nameof(userlogout));
                return userresponse;
            }
        }
    }
}