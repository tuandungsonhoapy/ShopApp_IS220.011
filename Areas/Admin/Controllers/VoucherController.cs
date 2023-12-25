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

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private readonly MyShopContext myShopContext;

        public VoucherController(MyShopContext myShop)
        {
            myShopContext = myShop;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            IEnumerable<VOUCHER> voucherinpage;
            int totalVoucher;
            totalVoucher = await myShopContext.VOUCHERs.CountAsync();
            if (pagesize <= 0) { pagesize = 10; }
            int countPages = (int)Math.Ceiling((double)totalVoucher / pagesize);


            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingmodel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) =>
                {
                    string? v = Url.Action("Index", new
                    {
                        p = pageNumber,
                        pagesize = pagesize,
                    });
                    return v;
                }
            };
            ViewData["currentpage_product"] = currentPage;
            ViewData["pagesize_product"] = pagesize;

            ViewBag.pagingmodel = pagingmodel;

            voucherinpage = await myShopContext.VOUCHERs.Skip((currentPage - 1) * pagesize)
                                                          .Take(pagesize)
                                                          .ToListAsync();
            return View(voucherinpage);
        }
         
        [HttpGet("/voucher/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/voucher/create")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TENVOUCHER, THOIGIANBD, THOIGIANKT, SOLUONG, MOTA, GIATRIGIAM, GIADONTOITHIEU, GIAMTOIDA, LOAIVOUCHER")]VOUCHER vOUCHER)
        {
            myShopContext.Add(vOUCHER);
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Tạo voucher thành công!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int voucherid)
        {
            var voucher = await myShopContext.VOUCHERs.FindAsync(voucherid);
            if(voucher != null)
            {
                myShopContext.Remove(voucher);
            }
            else return NotFound("Không có voucher cần xóa!");
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Xóa voucher " + voucher.TENVOUCHER + " thành công!";
            return Json(new { success = true, message = "Voucher đã được xóa thành công." });
        }

    }
}
