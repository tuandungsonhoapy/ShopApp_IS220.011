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
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DAFW_IS220.Controllers
{
    public static class ControllerExtensionsOrder
    {
        public static string RenderPartialViewToString(this OrderController controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var engine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                var viewResult = engine.FindView(controller.ControllerContext, viewName, false);

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }

    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        private readonly UserManager<AppUser> userManager;

        private readonly CartService cartService;

        private readonly MyShopContext myShopContext;

        private readonly IConfiguration configuration;

        private readonly IHttpContextAccessor httpContext;

        public OrderController(ILogger<OrderController> logger, UserManager<AppUser> user, CartService _cartService, MyShopContext dbcontext, IConfiguration _config, IHttpContextAccessor _httpContext)
        {
            _logger = logger;
            userManager = user;
            cartService = _cartService;
            myShopContext = dbcontext;
            configuration = _config;
            httpContext = _httpContext;
        }

        [Route("/index", Name = "index")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user != null)
            {
                ViewBag.userID = user.Id;
                userID = user.Id;
                var userAddress = user.DIACHI;
                if (userAddress != null) ViewBag.userAddress = userAddress;
                else ViewBag.userAddress = "NoAddress";
                ViewBag.userPhone = user.PhoneNumber;
                ViewBag.TENKH = user.TENKH;
            }
            var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();
            return View(cart);
        }

        [Route("/changeaddress", Name = "changeaddress")]
        public async Task<IActionResult> ChangeAddress([FromForm] string newaddress)
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";
            if (user.DIACHI == null)
            {
                TempData["StatusMessage"] = "Thêm địa chỉ thành công!";
            }
            else TempData["StatusMessage"] = "Thay đổi địa chỉ thành công!";

            if (user != null)
            {
                ViewBag.userID = user.Id;
                userID = user.Id;
                ViewBag.userAddress = newaddress;
                // var userAddress = user.DIACHI;
                // if(userAddress != null) ViewBag.userAddress = userAddress;
                // else ViewBag.userAddress = "NoAddress";
                ViewBag.userPhone = user.PhoneNumber;
                ViewBag.TENKH = user.TENKH;
                user.DIACHI = newaddress;
                myShopContext.Update(user);
                await myShopContext.SaveChangesAsync();
            }


            var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();
            // var updatedCartHtml = this.RenderPartialViewToString("_Cart", cart);
            return Ok();
        }


        [Route("/payment", Name = "payment")]
        public IActionResult Payment()
        {
            var user = userManager.GetUserAsync(User).Result;
            if (user != null)
            {
                var userID = user.Id;
                TempData["StatusMessage"] = userID;
            }
            return View();
        }

        [Route("/checkout", Name = "checkout")]
        public async Task<IActionResult> CheckOut(OrderModel orderModel)
        {
            var code = new { success = false, Code = -1, Url = "" };
            var user = await userManager.GetUserAsync(User);
            string CustomerName = "";
            string Phone = "";
            string Address = "";
            string Email = "";
            string userID = "";
            DONHANG dONHANG = new DONHANG();
            var cart = cartService.GetCartItems();
            if (user != null)
            {
                userID = user.Id;
                CustomerName = user.TENKH;
                Phone = user.PhoneNumber ?? "";
                Address = user.DIACHI ?? "";
                Email = user.Email ?? "";
            }
            dONHANG.MATK = userID;
            dONHANG.NGAYMUA = DateTime.Now;
            orderModel.TENKH = CustomerName;
            orderModel.Phone = Phone;
            orderModel.Email = Email;
            orderModel.Address = Address;
            if (orderModel.TypePayment == 1)
            {
                dONHANG.HINHTHUCTHANHTOAN = "COD";
                dONHANG.TRANGTHAITHANHTOAN = "Chưa thanh toán";
            }
            else if (orderModel.TypePayment == 2)
            {
                dONHANG.HINHTHUCTHANHTOAN = "Chuyển khoản";
                dONHANG.TRANGTHAITHANHTOAN = "Đã thanh toán";
            }
            dONHANG.TONGTIEN = orderModel.Price;
            dONHANG.TRANGTHAIDONHANG = "Chờ lấy hàng";
            dONHANG.GHICHU = orderModel.Note ?? "";
            dONHANG.NGAYSUADOI = DateTime.Now;
            myShopContext.Add(dONHANG);
            await myShopContext.SaveChangesAsync();
            foreach (var item in cart)
            {
                CTDH cTDH = new CTDH();
                cTDH.MADH = dONHANG.MADH;
                cTDH.MACTSP = item.product.MACTSP;
                cTDH.TONGGIA = item.quantity * item.product.GIABAN;
                cTDH.SOLUONG = item.quantity;
                myShopContext.Add(cTDH);
                //Cập nhật lại số lượng sản phẩm
                CHITIETSANPHAM cHITIETSANPHAM = await myShopContext.CHITIETSANPHAMs.FindAsync(cTDH.MACTSP) ?? new CHITIETSANPHAM();
                cHITIETSANPHAM.SOLUONG = cHITIETSANPHAM.SOLUONG - cTDH.SOLUONG;
                myShopContext.Update(cHITIETSANPHAM);
                await myShopContext.SaveChangesAsync();
            }
            cartService.ClearCart();
            if (orderModel.TypePayment == 2)
            {
                var url = UrlPayment(orderModel.TypePaymentVN, dONHANG.MADH);
                // code = new { success = true, Code = orderModel.TypePayment, Url = url };
                return Ok(new { success = true, code = orderModel.TypePayment, url = url });
            }
            else return Ok(new { success = true, Code = orderModel.TypePayment, Url = "" });
        }

        #region Thanh toán vnpay
        public string UrlPayment(int TypePaymentVN, int orderCode)
        {
            var urlPayment = "";
            //var sorder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
            var donhang = myShopContext.DONHANGs.FirstOrDefault(o => o.MADH == orderCode);
            OrderInfo order = new OrderInfo();
            order.OrderId = donhang != null ? donhang.MADH : 0;
            order.Amount = donhang != null ? (long)donhang.TONGTIEN * 100 : 0;
            order.Status = "0";
            order.CreatedDate = DateTime.Now;
            var vnpConfig = configuration.GetSection("VNPAY_SETTINGS");
            //Get Config Info
            // string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            // string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            // string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            // string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key
            string vnp_Returnurl = vnpConfig["vnp_Returnurl"] ?? "";
            string vnp_Url = vnpConfig["vnp_Url"] ?? "";
            string vnp_TmnCode = vnpConfig["vnp_TmnCode"] ?? "";
            string vnp_HashSecret = vnpConfig["vnp_HashSecret"] ?? "";
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)donhang.TONGTIEN * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", order.Amount.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", new Utils(httpContext).GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            _logger.LogInformation("VNPAY URL: {0}", urlPayment);
            return urlPayment;
        }
        #endregion
    }
}