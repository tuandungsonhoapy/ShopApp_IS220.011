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
        public ActionResult GetStatistical(string fromDate, string toDate)
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
                            Quantity = orderdetail.SOLUONG,
                            Price = orderdetail.TONGGIA,
                            OriginalPrice = product.GIABAN
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate < endDate);
            }

            var result = query
               .GroupBy(x => EntityFunctions.TruncateTime(x.CreatedDate))
                .Select(x => new
                {
                    Date = x.Key,
                    TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                    TotalSell = x.Sum(y => y.Quantity * y.Price),
                })
                .Select(x => new
                {
                    Date = x.Date,
                    DoanhThu = x.TotalSell,
                    LoiNhuan = x.TotalSell
                });

            return Json(new { Data = result });
        }
    }
}