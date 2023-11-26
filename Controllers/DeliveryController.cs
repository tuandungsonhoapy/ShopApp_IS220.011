using Microsoft.AspNetCore.Mvc;

namespace DAFW_IS220.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController (ILogger<DeliveryController> logger)
        {
            _logger = logger;
        }

        public IActionResult DangGiao()
        {
            return View();
        }

        public IActionResult DaGiao()
        {
            return View();
        }

        public IActionResult DaHuy()
        {
            return View();
        }

        public IActionResult FeedBack()
        {
            return View();
        }
    }
}