using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DAFW_IS220.Models;
using App.DBModels;

namespace DAFW_IS220.Controllers
{
    public class DanhMucTTController : Controller
    {
        private readonly ILogger<DanhMucTTController> _logger;

        private readonly MyShopContext myShopContext;

        public DanhMucTTController(ILogger<DanhMucTTController> logger, MyShopContext _myshopcontext)
        {
            _logger = logger;
            myShopContext = _myshopcontext;
        }

        public IActionResult Index()
        {
            var products = (from product in myShopContext.SANPHAMs
                            join urlanh in myShopContext.HINHANHs
                            on product.MASP equals urlanh.MASP
                            select new SANPHAMModel()
                            {
                                MASP = product.MASP,
                                TENSP = product.TENSP,
                                GIAGOC = product.GIAGOC,
                                GIABAN = product.GIABAN,
                                HINHANH = urlanh.LINK
                            })
                        .GroupBy(p => p.MASP)
                        .Select(g => g.First())
                        .ToList();
            return View(products);
        }

        public IActionResult TTNam()
        {
            var products = (from product in myShopContext.SANPHAMs
                            join urlanh in myShopContext.HINHANHs
                            on product.MASP equals urlanh.MASP
                            select new SANPHAMModel()
                            {
                                MASP = product.MASP,
                                TENSP = product.TENSP,
                                GIAGOC = product.GIAGOC,
                                GIABAN = product.GIABAN,
                                HINHANH = urlanh.LINK
                            })
                        .GroupBy(p => p.MASP)
                        .Select(g => g.First())
                        .ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

