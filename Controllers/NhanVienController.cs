using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OnTapCK.Controllers;

public class NhanVienController : Controller
{
    private readonly ILogger<NhanVienController> _logger;

    public NhanVienController(ILogger<NhanVienController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() 
    {
        return View();
    }

    public IActionResult LietKeNV(int SoLanSua){
        DataContext data = new DataContext();
        var model = data.LietKeNV(SoLanSua);
        return View(model);
    }

}
