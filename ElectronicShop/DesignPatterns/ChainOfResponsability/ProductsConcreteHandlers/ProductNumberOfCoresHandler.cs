using ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase;
using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.ProductsConcreteHandlers
{
    public class ProductNumberOfCoresHandler : AddProductHandlerBase
    {
        public override AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO laptop)
        {
            try
            {
                if ((laptop.numberofcores % 4 == 0 || laptop.numberofcores % 6 == 0) && laptop.numberofcores <= 12 && laptop.numberofcores > 0)
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
                if ((phone.numberofcores % 4 == 0 || phone.numberofcores % 6 == 0) && phone.numberofcores <= 8 && phone.numberofcores > 0)
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