using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DAFW_IS220.Models;
using App.DBModels;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace DAFW_IS220.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private readonly ILogger<DeliveryController> _logger;

        private readonly MyShopContext myShopContext;

        private readonly UserManager<AppUser> userManager;

        public DeliveryController(ILogger<DeliveryController> logger, MyShopContext myShop, UserManager<AppUser> manager)
        {
            _logger = logger;
            myShopContext = myShop;
            userManager = manager;
        }

        public async Task<IActionResult> Index()
        {
            // var orderdetail = myShopContext.CTDHs.Include(order => order.DONHANG);
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   select new OrderDetailModel()
                                   {
                                       MADH = orderdetail.MADH,
                                       cHITIETSANPHAM = productdetail,
                                       Price = orderdetail.TONGGIA,
                                       SoLuong = orderdetail.SOLUONG,
                                       sANPHAM = product,
                                       mAUSAC = color,
                                       sIZE = size
                                   }).ToList();
            var orders = myShopContext.DONHANGs.Where(dh => dh.MATK.Equals(userID) && dh.TRANGTHAIDONHANG.Equals("Chờ lấy hàng")).OrderByDescending(dh => dh.NGAYSUADOI).ToList();
            var orderList = orders.ToList().GroupJoin(orderdetailList, o => o.MADH, od => od.MADH, (o, ods) =>
        {
            return new OrderListModel
            {
                MADH = o.MADH,
                OrderStatus = o.TRANGTHAIDONHANG,
                TONGTIEN = o.TONGTIEN,
                OrderDetails = ods.ToList()
            };
        }).ToList();
            return View(orderList);
        }

        public async Task<IActionResult> DangGiao()
        {
            // var orderdetail = myShopContext.CTDHs.Include(order => order.DONHANG);
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   select new OrderDetailModel()
                                   {
                                       MADH = orderdetail.MADH,
                                       cHITIETSANPHAM = productdetail,
                                       Price = orderdetail.TONGGIA,
                                       SoLuong = orderdetail.SOLUONG,
                                       sANPHAM = product,
                                       mAUSAC = color,
                                       sIZE = size
                                   }).ToList();
            var orders = myShopContext.DONHANGs.Where(dh => dh.MATK.Equals(userID) && (dh.TRANGTHAIDONHANG.Equals("Đang giao") || dh.TRANGTHAIDONHANG.Equals("Chờ xác nhận"))).OrderByDescending(dh => dh.NGAYSUADOI).ToList();
            var orderList = orders.ToList().GroupJoin(orderdetailList, o => o.MADH, od => od.MADH, (o, ods) =>
        {
            return new OrderListModel
            {
                MADH = o.MADH,
                OrderStatus = o.TRANGTHAIDONHANG,
                TONGTIEN = o.TONGTIEN,
                OrderDetails = ods.ToList()
            };
        }).ToList();
            return View(orderList);
        }

        public async Task<IActionResult> DaGiao()
        {
            // var orderdetail = myShopContext.CTDHs.Include(order => order.DONHANG);
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   select new OrderDetailModel()
                                   {
                                       MADH = orderdetail.MADH,
                                       cHITIETSANPHAM = productdetail,
                                       Price = orderdetail.TONGGIA,
                                       SoLuong = orderdetail.SOLUONG,
                                       sANPHAM = product,
                                       mAUSAC = color,
                                       sIZE = size
                                   }).ToList();
            var orders = myShopContext.DONHANGs.Where(dh => dh.MATK.Equals(userID) && dh.TRANGTHAIDONHANG.Equals("Đã giao")).OrderByDescending(dh => dh.NGAYSUADOI).ToList();
            var orderList = orders.ToList().GroupJoin(orderdetailList, o => o.MADH, od => od.MADH, (o, ods) =>
        {
            return new OrderListModel
            {
                MADH = o.MADH,
                OrderStatus = o.TRANGTHAIDONHANG,
                TONGTIEN = o.TONGTIEN,
                OrderDetails = ods.ToList()
            };
        }).ToList();
            return View(orderList);
        }

        public async Task<IActionResult> DaHuy()
        {
            // var orderdetail = myShopContext.CTDHs.Include(order => order.DONHANG);
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   select new OrderDetailModel()
                                   {
                                       MADH = orderdetail.MADH,
                                       cHITIETSANPHAM = productdetail,
                                       Price = orderdetail.TONGGIA,
                                       SoLuong = orderdetail.SOLUONG,
                                       sANPHAM = product,
                                       mAUSAC = color,
                                       sIZE = size
                                   }).ToList();
            var orders = myShopContext.DONHANGs.Where(dh => dh.MATK.Equals(userID) && dh.TRANGTHAIDONHANG.Equals("Đã hủy")).OrderByDescending(dh => dh.NGAYSUADOI).ToList();
            var orderList = orders.ToList().GroupJoin(orderdetailList, o => o.MADH, od => od.MADH, (o, ods) =>
        {
            return new OrderListModel
            {
                MADH = o.MADH,
                OrderStatus = o.TRANGTHAIDONHANG,
                TONGTIEN = o.TONGTIEN,
                OrderDetails = ods.ToList()
            };
        }).ToList();
            return View(orderList);
        }

        public async Task<IActionResult> HoanThanh(int id)
        {
            var order = await myShopContext.DONHANGs.FindAsync(id);
            order.TRANGTHAIDONHANG = "Đã giao";
            order.TRANGTHAITHANHTOAN = "Đã thanh toán";
            order.NGAYSUADOI = DateTime.Now;
            THANHTOAN tHANHTOAN = new THANHTOAN();
            tHANHTOAN.MADH = order.MADH;
            tHANHTOAN.SOTIEN = order.TONGTIEN;
            tHANHTOAN.NGAYTHANHTOAN = DateTime.Now;
            myShopContext.Add(tHANHTOAN);
            myShopContext.Update(order);
            await myShopContext.SaveChangesAsync();
            return RedirectToAction(nameof(DaGiao));
        }

        [HttpGet]
        public async Task<IActionResult> FeedBack(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            ViewBag.MADH = id;
            var feedbacks = myShopContext.DANHGIAs.Where(fb => fb.MADH == id).ToList();
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   where orderdetail.MADH == id
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   select new OrderDetailModel()
                                   {
                                       MADH = orderdetail.MADH,
                                       cHITIETSANPHAM = productdetail,
                                       Price = orderdetail.TONGGIA,
                                       SoLuong = orderdetail.SOLUONG,
                                       sANPHAM = product,
                                       mAUSAC = color,
                                       sIZE = size
                                   }).ToList();
            var combinedList = from orderdetail in orderdetailList
                               join feedback in feedbacks
                               on orderdetail.sANPHAM.MASP equals feedback.MASP
                               into feedbackGroup
                               from feedback in feedbackGroup.DefaultIfEmpty() // Left join
                               select new OrderFeedback()
                               {
                                   orderDetailModel = orderdetail,
                                   dANHGIA = feedback
                               };
            var result = combinedList.ToList();
            return View(result);
        }

        [Route("/confirmfeedback", Name = "confirmfeedback")]
        public async Task<IActionResult> ConfirmFeedback([FromForm] int productdetailid, [FromForm] int rating, [FromForm] string feedback, [FromForm] int orderid)
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var productdetail = await myShopContext.CHITIETSANPHAMs.FindAsync(productdetailid);
            DANHGIA dANHGIA = new DANHGIA();
            dANHGIA.MATK = userID;
            dANHGIA.MASP = productdetail.MASP;
            dANHGIA.SOSAO = rating;
            dANHGIA.NOIDUNG = feedback;
            dANHGIA.MADH = orderid;
            myShopContext.Add(dANHGIA);
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Đánh giá thành công!";
            return Ok(new { success = true, orderid });
        }


        [Route("/updatefeedback", Name = "updatefeedback")]
        public async Task<IActionResult> UpdateFeedback([FromForm] int productdetailid, [FromForm] int rating, [FromForm] string feedback, [FromForm] int orderid, [FromForm] int productid)
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            DANHGIA dANHGIA = myShopContext.DANHGIAs.Where(fb => fb.MASP == productid && fb.MADH == orderid && fb.MATK.Equals(userID)).FirstOrDefault() ?? new DANHGIA();
            if (dANHGIA == null)
            {
                TempData["StatusMessage"] = "Không có đánh giá cần cập nhật!";
                return Ok(new { success = true, orderid });
            }
            else
            {
                dANHGIA.SOSAO = rating;
                dANHGIA.NOIDUNG = feedback;
                myShopContext.Update(dANHGIA);
                await myShopContext.SaveChangesAsync();
                TempData["StatusMessage"] = "Cập nhật đánh giá thành công!";
                return Ok(new { success = true, orderid });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Order = await myShopContext.DONHANGs.FindAsync(id);
            if (Order != null)
            {
                Order.TRANGTHAIDONHANG = "Đã hủy";
                Order.NGAYSUADOI = DateTime.Now;
                myShopContext.Update(Order);
            }

            var orderdetailList = (from ctdh in myShopContext.CTDHs
                                   join ctsp in myShopContext.CHITIETSANPHAMs
                                   on ctdh.MACTSP equals ctsp.MACTSP
                                   where ctdh.MADH == id
                                   select new CTDH(){
                                    MADH = ctdh.MADH,
                                    MACTSP = ctdh.MACTSP,
                                    TONGGIA = ctdh.TONGGIA,
                                    SOLUONG = ctdh.SOLUONG,
                                    CHITIETSANPHAM = ctsp
                                   }).ToList();
            foreach (var item in orderdetailList)
            {
                CHITIETSANPHAM cHITIETSANPHAM = item.CHITIETSANPHAM;
                cHITIETSANPHAM.SOLUONG = cHITIETSANPHAM.SOLUONG + item.SOLUONG;
                myShopContext.Update(cHITIETSANPHAM);
            }
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Hủy đơn hàng thành công!";
            return RedirectToAction(nameof(DaHuy));
        }

        public async Task<IActionResult> buyAgain(int id)
        {
            var Order = await myShopContext.DONHANGs.FindAsync(id);
            if (Order != null)
            {
                Order.TRANGTHAIDONHANG = "Chờ lấy hàng";
                Order.NGAYSUADOI = DateTime.Now;
                myShopContext.Update(Order);
            }

            var orderdetailList = (from ctdh in myShopContext.CTDHs
                                   join ctsp in myShopContext.CHITIETSANPHAMs
                                   on ctdh.MACTSP equals ctsp.MACTSP
                                   where ctdh.MADH == id
                                   select new CTDH(){
                                    MADH = ctdh.MADH,
                                    MACTSP = ctdh.MACTSP,
                                    TONGGIA = ctdh.TONGGIA,
                                    SOLUONG = ctdh.SOLUONG,
                                    CHITIETSANPHAM = ctsp
                                   }).ToList();
            foreach (var item in orderdetailList)
            {
                CHITIETSANPHAM cHITIETSANPHAM = item.CHITIETSANPHAM;
                cHITIETSANPHAM.SOLUONG = cHITIETSANPHAM.SOLUONG - item.SOLUONG;
                myShopContext.Update(cHITIETSANPHAM);
            }
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Đặt lại đơn hàng thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}