namespace ElectronicShop.ShopModels.DTOModel.GetDTO
{
    public class getorderdetailsDTO
    {
        public int orderdetailID { get; set; }
        public int orderID { get; set; }
        public int phoneproductID { get; set; }
        public DateTime boughtdate { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
}