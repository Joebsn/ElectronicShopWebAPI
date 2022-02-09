using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers
{
    public class ProductStorageHandler : AddProductHandlerBase
    {
        public override AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO laptop)
        {
            try
            {
                if (laptop.storage % 128 == 0 && laptop.storage > 0 && laptop.storage <= 2000)
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
                if (phone.storage % 32 == 0 && phone.storage > 0 && phone.storage <= 256)
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