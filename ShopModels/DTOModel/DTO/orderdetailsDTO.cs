namespace ElectronicShop.ShopModels.DTOModel.DTO
{
    public class orderdetailsDTO
    {
        public int orderdetailID { get; set; }
        public int orderID { get; set; }
        public int electronicproductID { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
}