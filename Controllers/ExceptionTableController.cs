using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop.Controllers
{
    public class ExceptionTableController : Controller
    {
        private readonly ILogger<ExceptionTableController> _logger;
        public ExceptionTableController(ILogger<ExceptionTableController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");
        }
    }
}
