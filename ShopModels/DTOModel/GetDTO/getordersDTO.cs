namespace ElectronicShop.ShopModels.DTOModel.GetDTO
{
    public class getordersDTO
    {
        public int orderID { get; set; }
        public int userID { get; set; }
        public int totalnumberofobjectsbought { get; set; }
        public float totalprice { get; set; }
    }
}