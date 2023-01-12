using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers
{
    public class ProductMemoryHandler : AddProductHandlerBase
    {
        public override AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO laptop)
        {
            try
            {
                if (laptop.memory % 4 == 0 && laptop.memory <= 32 && laptop.memory > 0)
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
                if (phone.memory % 4 == 0 && phone.memory > 0 && phone.memory <= 8)
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