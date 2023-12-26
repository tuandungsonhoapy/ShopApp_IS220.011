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
using System.Data.Entity.Core.Objects;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly MyShopContext myShopContext;

        public StatisticController(MyShopContext myShop)
        {
            myShopContext = myShop;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStatistical([FromForm] DateTime fromDate, [FromForm] DateTime toDate)
        {
            var query = from order in myShopContext.DONHANGs
                        join orderdetail in myShopContext.CTDHs
                        on order.MADH equals orderdetail.MADH
                        join productdetail in myShopContext.CHITIETSANPHAMs
                        on orderdetail.MACTSP equals productdetail.MACTSP
                        join product in myShopContext.SANPHAMs
                        on productdetail.MASP equals product.MASP
                        select new
                        {
                            CreatedDate = order.NGAYMUA,
                            TotalPrice = order.TONGTIEN
                        };
            if (fromDate != DateTime.MinValue)
            {
                query = query.Where(x => x.CreatedDate >= fromDate);
            }
            if (toDate != DateTime.MinValue)
            {
                query = query.Where(x => x.CreatedDate < toDate);
            }

            // var result = query
            //    .GroupBy(x => EntityFunctions.TruncateTime(x.CreatedDate))
            //     .Select(x => new
            //     {
            //         Date = x.Key,
            //         TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
            //         TotalSell = x.Sum(y => y.Quantity * y.Price),
            //     })
            //     .Select(x => new
            //     {
            //         Date = x.Date,
            //         DoanhThu = x.TotalSell,
            //         LoiNhuan = x.TotalSell
            //     });
            var result = query
        .GroupBy(x => new { Year = x.CreatedDate.Year, Month = x.CreatedDate.Month })
        .Select(group => new
        {
            date = group.Key,
            price = group.Sum(x => x.TotalPrice)
        });
            return Json(new { success = true, data = result });
        }
    }
}