using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DBModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using App.Models;
using App.Areas.Identity.Models.ManageViewModels;
using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text.Json.Serialization;

namespace App.Areas.Admin.Controllers{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly MyShopContext myShopContext;

        public StatisticController(MyShopContext myShop)
        {
            myShopContext = myShop;
        }

        
    }
}