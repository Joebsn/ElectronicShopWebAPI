using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers
{
    public class ProductBatteryHandler : AddProductHandlerBase
    {
        public override AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO laptop)
        {
            try
            {
                if (laptop.battery >= 15 && laptop.battery <= 30)
                {
                    if (_successor == null) return AddElectronicProductResult.Accepted;
                    else return _successor.EvaluateLaptopSpecifications(laptop);
                }
                return AddElectronicProductResult.Rejected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override AddElectronicProductResult EvaluatePhoneSpecifications(ElectronicProductDTO phone)
        {
            try
            {
                if (phone.battery >= 15 && phone.battery <= 30)
                {
                    if (_successor == null) return AddElectronicProductResult.Accepted;
                    else return _successor.EvaluatePhoneSpecifications(phone);
                }
                return AddElectronicProductResult.Rejected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}