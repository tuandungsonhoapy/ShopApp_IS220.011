using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTapCK.Models;
using System.Linq;

namespace OnTapCK.Controllers;

public class HanhKhachController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HanhKhachController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() 
    {
        return View();
    }

    public IActionResult InsertHK(HANHKHACH hANHKHACH)
    {
        OnTapCKContext onTap = new OnTapCKContext();
        onTap.InsertHK(hANHKHACH);
        return View("Index");
    }
}
