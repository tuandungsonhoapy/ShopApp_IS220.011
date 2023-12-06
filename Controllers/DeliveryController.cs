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
            var orderdetailList = (from orderdetail in myShopContext.CTDHs
                                   join productdetail in myShopContext.CHITIETSANPHAMs
                                   on orderdetail.MACTSP equals productdetail.MACTSP
                                   join product in myShopContext.SANPHAMs
                                   on productdetail.MASP equals product.MASP
                                   join color in myShopContext.MAUSACs
                                   on productdetail.MAMAU equals color.MAMAU
                                   join size in myShopContext.SIZEs
                                   on productdetail.MASIZE equals size.MASIZE
                                   where orderdetail.MADH == id
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
            return View(orderdetailList);
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
            myShopContext.Add(dANHGIA);
            await myShopContext.SaveChangesAsync();
            TempData["StatusMessage"] = "Đánh giá thành công!";
            return Ok(new { success = true, orderid });
        }
    }
}