using App.DBModels;
using Microsoft.AspNetCore.Mvc;

namespace DAFW_IS220.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        private readonly MyShopContext myShopContext;

        public CartController (ILogger<CartController> logger, MyShopContext _myshopcontext)
        {
            _logger = logger;
            myShopContext = _myshopcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult InfoOrder()
        {
            return View();
        }
    }
}