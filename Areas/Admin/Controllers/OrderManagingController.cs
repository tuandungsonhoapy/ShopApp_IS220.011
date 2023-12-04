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

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class OrderManagingController : Controller
    {
        private readonly MyShopContext myShopContext;

        public OrderManagingController(MyShopContext myShop)
        {
            myShopContext = myShop;
        }

        [HttpGet("/admin/quanlyhoadon")]
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            IEnumerable<DONHANG> orderinpage;
            int totalOrder;
            totalOrder = await myShopContext.DONHANGs.CountAsync();
            if (pagesize <= 0) { pagesize = 10; }
            int countPages = (int)Math.Ceiling((double)totalOrder / pagesize);


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

            var orders = await myShopContext.DONHANGs.Include(o => o.TAIKHOAN).ToListAsync();
            orderinpage = await myShopContext.DONHANGs.Skip((currentPage - 1) * pagesize)
                                                          .Take(pagesize)
                                                          .Include(o => o.TAIKHOAN)
                                                          .ToListAsync();
            return View(orderinpage);
        }

        [HttpGet("/admin/order/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var order = await myShopContext.DONHANGs.FindAsync(id);
            return View(order);
        }

        [HttpPost("/admin/order/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MADH, TRANGTHAIDONHANG")]DONHANG donhang)
        {
            
            var order = await myShopContext.DONHANGs.FindAsync(id);
            order.TRANGTHAIDONHANG = donhang.TRANGTHAIDONHANG;
            order.NGAYSUADOI = DateTime.Now;
            // var orderdetails = await (from ord in myShopContext.DONHANGs
            //                           join ordetail in myShopContext.CTDHs
            //                           on ord.MADH equals ordetail.MADH
            //                           join prodetail in myShopContext.CHITIETSANPHAMs
            //                           on ordetail.MACTSP equals prodetail.MACTSP
            //                           select new {
            //                             OrderDetail = ordetail,
            //                             ProductDetail = prodetail
            //                           }).ToListAsync();
            var orderdetailList = await (from ordetail in myShopContext.CTDHs
                                         join prodetail in myShopContext.CHITIETSANPHAMs
                                         on ordetail.MACTSP equals prodetail.MACTSP
                                         where ordetail.MADH == id
                                         select new {
                                            OrderDetail = ordetail,
                                            ProductDetail = prodetail
                                         }).ToListAsync();
            foreach(var item in orderdetailList)
            {
                CHITIETSANPHAM cHITIETSANPHAM = new CHITIETSANPHAM();
                cHITIETSANPHAM = item.ProductDetail;
                cHITIETSANPHAM.SOLUONG = cHITIETSANPHAM.SOLUONG - item.OrderDetail.SOLUONG;
                myShopContext.Update(cHITIETSANPHAM);
                await myShopContext.SaveChangesAsync();
            }
            
            myShopContext.Update(order);
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Thay đổi trạng thái đơn hàng thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}