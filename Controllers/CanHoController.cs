using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OnTapCK.Controllers;

public class CanHoController : Controller
{
    private readonly ILogger<CanHoController> _logger;

    public CanHoController(ILogger<CanHoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() //Views/HanhKhach/Index
    {
        return View();
    }

    public IActionResult InsertCanHo(CANHO cANHO){
        DataContext data = new DataContext();
        data.InsertCH(cANHO);
        return View("Index");
    }

}
