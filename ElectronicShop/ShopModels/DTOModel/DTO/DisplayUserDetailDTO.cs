namespace ElectronicShop.ShopModels.DTOModel.DTO
{
    public class DisplayUserDetailDTO
    {
        public userDTO? Specificuser { get; set; }

        public ordersDTO? Allorders { get; set; }

        public orderdetailsDTO? AllOrderDetails { get; set; }

        public ElectronicProductDTO? electronicproductobject { get; set; }
    }
}
