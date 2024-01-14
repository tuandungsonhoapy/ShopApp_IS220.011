using System.Diagnostics;
using DB_CK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OnTapCK.Controllers;

public class NV_TBController : Controller
{
    private readonly ILogger<NV_TBController> _logger;

    public NV_TBController(ILogger<NV_TBController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() 
    {
        DataContext data = new DataContext();
        var model = data.GetNVs();
        return View(model);
    }

    public IActionResult LietKeTB(string MaNhanVien){
        DataContext data = new DataContext();
        var model = data.GetNV_BTs(MaNhanVien);
        return View(model);
    }

    public IActionResult DeleteNV_TB(NV_BT nV_BT){
        DataContext data = new DataContext();
        data.DeleteNV_TB(nV_BT);
        return RedirectToAction("LietKeTB", new { MaNhanVien = nV_BT.MaNhanVien });
    }

    public IActionResult Show_NV_BT(NV_BT nV_BT){
        DataContext data = new DataContext();
        var model = data.GetNV_BT(nV_BT);
        return View(model);
    }

    public IActionResult UpdateNV_BT(NV_BT nV_BT){
        DataContext data = new DataContext();
        data.UpdateNV_BT(nV_BT);
        return RedirectToAction("LietKeTB", new { MaNhanVien = nV_BT.MaNhanVien });
    }
}
