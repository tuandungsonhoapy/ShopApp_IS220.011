using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.DBModels;
using App.Areas.Identity.Models.ManageViewModels;
using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        private readonly ILogger<HomeAdminController> _logger;

        private readonly MyShopContext _myshopcontext;

        public HomeAdminController(ILogger<HomeAdminController> logger, MyShopContext myShopContext)
        {
            _logger = logger;
            _myshopcontext = myShopContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}