using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DAFW_IS220.Models;
using App.DBModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAFW_IS220.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly MyShopContext myshopcontext;

    public HomeController(ILogger<HomeController> logger, MyShopContext myShopContext)
    {
        _logger = logger;
        myshopcontext = myShopContext;
    }

    public IActionResult Index()
    {
        return View(myshopcontext.SANPHAMs);
    }

    public IActionResult TTNAM()
    {
        var products = (from product in myshopcontext.SANPHAMs
                        where product.PLTHOITRANG.Equals("Nam")
                        join urlanh in myshopcontext.HINHANHs
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

    public IActionResult TTNU()
    {
        var products = (from product in myshopcontext.SANPHAMs
                        where product.PLTHOITRANG.Equals("Nữ")
                        join urlanh in myshopcontext.HINHANHs
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

    public IActionResult TTTREEM()
    {
        var products = (from product in myshopcontext.SANPHAMs
                        where product.PLTHOITRANG.Equals("Trẻ em")
                        join urlanh in myshopcontext.HINHANHs
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

    public IActionResult Login()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
