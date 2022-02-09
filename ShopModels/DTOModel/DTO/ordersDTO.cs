namespace ElectronicShop.ShopModels.DTOModel.DTO
{
    public class ordersDTO
    {
        public int orderID { get; set; }
        public int userID { get; set; }
        public DateTime boughtdate { get; set; }
        public int totalnumberofobjectsbought { get; set; }
        public float totalprice { get; set; }
    }
}