using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTapCK.Models;
using System.Linq;

namespace OnTapCK.Controllers;

public class ChuyenBayController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ChuyenBayController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Show_Info_CB(string MACH)
    {
        OnTapCKContext onTap = new OnTapCKContext();
        ViewBag.CHUYENBAY = onTap.GetCHUYENBAY(MACH);
        ViewBag.THUONG = onTap.GetChoThuong(MACH);
        ViewBag.VIP = onTap.GetChoVIP(MACH);
        var HKs = onTap.GetHanhKhachs(MACH);
        return View(HKs);
    }
}
