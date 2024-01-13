using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTapCK.Models;
using System.Linq;

namespace OnTapCK.Controllers;

public class CT_CBController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public CT_CBController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult AddHK(string MACH){
        OnTapCKContext onTap = new OnTapCKContext();
        var CB = onTap.GetCHUYENBAY(MACH);
        ViewBag.THUONG = onTap.GetChoThuong(MACH);
        ViewBag.VIP = onTap.GetChoVIP(MACH);
        return View(CB);
    }

    public IActionResult InsertHK_CB(CT_CB cT_CB){
        OnTapCKContext onTap = new OnTapCKContext();
        onTap.InsertHK_CB(cT_CB);
        return RedirectToAction("AddHK", new { MACH = cT_CB.MACH });
    }

    public IActionResult ShowUpdateHK(string MAHK, string MACH){
        OnTapCKContext onTap = new OnTapCKContext();
        ViewBag.HANHKHACH = onTap.GetHANHKHACH(MAHK);
        var model = onTap.GetCT_CB(MACH, MAHK);
        return View(model);
    }
}
