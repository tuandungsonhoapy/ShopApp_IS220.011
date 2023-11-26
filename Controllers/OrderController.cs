using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DAFW_IS220.Models;
using App.DBModels;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DAFW_IS220.Controllers
{
    public static class ControllerExtensionsOrder
    {
        public static string RenderPartialViewToString(this OrderController controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var engine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                var viewResult = engine.FindView(controller.ControllerContext, viewName, false);

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }

    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        private readonly UserManager<AppUser> userManager;

        private readonly CartService cartService;

        public OrderController(ILogger<OrderController> logger, UserManager<AppUser> user, CartService _cartService)
        {
            _logger = logger;
            userManager = user;
            cartService = _cartService;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(User).Result;
            if(user!=null)
            {
                ViewBag.userID = user.Id;
                var userAddress = user.DIACHI;
                if(userAddress != null) ViewBag.userAddress = userAddress;
                else ViewBag.userAddress = "NoAddress";
                ViewBag.userPhone = user.PhoneNumber;
                ViewBag.TENKH = user.TENKH;
            }
            var cart = cartService.GetCartItems();
            return View(cart);
        }

        [Route("/changeaddress", Name = "changeaddress")]
        public IActionResult ChangeAddress()
        {
            var user = userManager.GetUserAsync(User).Result;
            if(user!=null)
            {
                var userID = user.Id;
                TempData["StatusMessage"] = userID;
            }
            return View();
        }


        [Route("/payment", Name = "payment")]
        public IActionResult Payment()
        {
            var user = userManager.GetUserAsync(User).Result;
            if(user!=null)
            {
                var userID = user.Id;
                TempData["StatusMessage"] = userID;
            }
            return View();
        }
    }
}