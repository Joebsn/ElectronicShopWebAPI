using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers
{
    public class ProductPriceHandler : AddProductHandlerBase 
    {
        public override AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO laptop)
        {
            try
            {
                if (laptop.price >= 500 && laptop.price <= 2500)
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
                if (phone.price >= 550 && phone.price <= 1400)
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