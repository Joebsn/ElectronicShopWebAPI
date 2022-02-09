using ElectronicShop.ShopModels.DTOModel.DTO;

namespace ElectronicShop.DesignPatterns.ChainOfResponsability.HandlerBase
{
    public abstract class AddProductHandlerBase
    {
        protected AddProductHandlerBase? _successor;

        public void SetSuccessor(AddProductHandlerBase successor)
        {
            _successor = successor;
        }

        public abstract AddElectronicProductResult EvaluateLaptopSpecifications(ElectronicProductDTO product);

        public abstract AddElectronicProductResult EvaluatePhoneSpecifications(ElectronicProductDTO product);
    }

    public enum AddElectronicProductResult
    {
        Accepted,
        Rejected
    }
}
