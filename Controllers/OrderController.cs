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
using Newtonsoft.Json;

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

        public List<CartItem> GetCartItemList(List<CHITIETGIOHANG> list)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }

            var cartList = (from detailcart in myShopContext.CHITIETGIOHANGs
                            where detailcart.MATK.Equals(userID)
                            join productdetail in myShopContext.CHITIETSANPHAMs
                            on detailcart.MACTSP equals productdetail.MACTSP
                            join color in myShopContext.MAUSACs
                            on productdetail.MAMAU equals color.MAMAU
                            join size in myShopContext.SIZEs
                            on productdetail.MASIZE equals size.MASIZE
                            join product in myShopContext.SANPHAMs
                            on detailcart.MASP equals product.MASP
                            select new ProductDetailModel()
                            {
                                MASP = productdetail.MASP,
                                MACTSP = detailcart.MACTSP,
                                TENSP = product.TENSP,
                                GIABAN = product.GIABAN,
                                GIAGOC = product.GIAGOC,
                                MAMAU = color.MAMAU,
                                TENMAU = color.TENMAU,
                                HEX = color.HEX,
                                MASIZE = size.MASIZE,
                                Size = size.Size,
                                SoLuong = detailcart.SOLUONGMUA,
                                MainImg = product.MainImg
                            }
                            ).Select(p => new CartItem()
                            {
                                userid = userID,
                                quantity = p.SoLuong,
                                product = p
                            }).ToList();
            return cartList;
        }
        // [Route("/index", Name = "index")]
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
            ViewBag.DeliveryInfo = myShopContext.THONGTINGIAOHANGs.Where(s => s.MATK.Equals(userID)).ToList();
            // var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();

            var cartList = GetCartItemList(myShopContext.CHITIETGIOHANGs.ToList()).Where(p => p.userid.Equals(userID)).ToList();
            return View(cartList);
        }

        public async Task<IActionResult> DiscountByVoucher([FromForm] string code)
        {
            var result = await myShopContext.VOUCHERs.Where(v => v.TENVOUCHER.Equals(code)).FirstOrDefaultAsync();
            if (result != null)
            {
                int DiscountPercent = result.GIATRIGIAM;
                result.SOLUONG--;
                myShopContext.Update(result);
                await myShopContext.SaveChangesAsync();
                return Ok(new { success = true, discountnumber = DiscountPercent });
            }
            return Ok(new { success = true, discountnumber = 0 });
        }

        [HttpPost]
        [Route("/buynow", Name = "buynow")]
        public async Task<IActionResult> BuyNow([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity, [FromForm] int productdetailid)
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

            var color = myShopContext.MAUSACs
                        .Where(c => c.MAMAU == colorid)
                        .FirstOrDefault();
            var size = myShopContext.SIZEs
                       .Where(s => s.MASIZE == sizeid)
                       .FirstOrDefault();
            var product = myShopContext.SANPHAMs
                          .Where(p => p.MASP == productid)
                          .Select(p => new ProductDetailModel()
                          {
                              MASP = p.MASP,
                              MACTSP = productdetailid,
                              TENSP = p.TENSP,
                              GIABAN = p.GIABAN,
                              GIAGOC = p.GIAGOC,
                              MAMAU = color.MAMAU,
                              TENMAU = color.TENMAU,
                              HEX = color.HEX,
                              MASIZE = size.MASIZE,
                              Size = size.Size,
                              SoLuong = quantity,
                              MainImg = p.MainImg
                          })
                          .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Thêm vào session
            cartService.ClearCart();
            var cart = cartService.GetCartItems();
            cart.Add(new CartItem() { quantity = product.SoLuong, product = product, userid = userID });
            cartService.SaveCartSession(cart);
            return Ok();
        }

        public async Task<IActionResult> PageBuyNow()
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
        public async Task<IActionResult> ChangeAddress([FromForm] string addressValue, [FromForm] string phoneNumber, [FromForm] string receiverName)
        {
            var user = await userManager.GetUserAsync(User);
            var userID = "null";

            if (user != null)
            {
                ViewBag.userID = user.Id;
                userID = user.Id;
                ViewBag.userAddress = addressValue;
                // var userAddress = user.DIACHI;
                // if(userAddress != null) ViewBag.userAddress = userAddress;
                // else ViewBag.userAddress = "NoAddress";
                ViewBag.userPhone = user.PhoneNumber;
                ViewBag.TENKH = user.TENKH;
                user.DIACHI = addressValue;
                myShopContext.Update(user);
                THONGTINGIAOHANG tHONGTINGIAOHANG = new THONGTINGIAOHANG();
                tHONGTINGIAOHANG.MATK = userID;
                tHONGTINGIAOHANG.NGAYTAO = DateTime.Now;
                tHONGTINGIAOHANG.SDT = phoneNumber;
                tHONGTINGIAOHANG.DIACHI = addressValue;
                tHONGTINGIAOHANG.TENNGUOINHAN = receiverName;
                myShopContext.Add(tHONGTINGIAOHANG);
                await myShopContext.SaveChangesAsync();
            }


            // var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();
            // var updatedCartHtml = this.RenderPartialViewToString("_Cart", cart);
            return Ok();
        }

        [Route("/updateaddress", Name = "updateaddress")]
        public async Task<IActionResult> UpdateAddress([FromForm] string addressValue)
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                user.DIACHI = addressValue;
                myShopContext.Update(user);
                await myShopContext.SaveChangesAsync();
            }
            // var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();
            // var updatedCartHtml = this.RenderPartialViewToString("_Cart", cart);
            return Ok();
        }

        [Route("/deleteaddress", Name = "deleteaddress")]
        public async Task<IActionResult> DeleteAddress([FromForm] int deliveryid)
        {
            var deliveryInfo = myShopContext.THONGTINGIAOHANGs.Find(deliveryid);

            if (deliveryInfo != null)
            {
                myShopContext.Remove(deliveryInfo);
                await myShopContext.SaveChangesAsync();
            }
            // var cart = cartService.GetCartItems().Where(p => p.userid.Equals(userID)).ToList();
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
            // var cart = cartService.GetCartItems();
            if (user != null)
            {
                userID = user.Id;
                CustomerName = user.TENKH;
                Phone = user.PhoneNumber ?? "";
                Address = user.DIACHI ?? "";
                Email = user.Email ?? "";
            }
            var cart = GetCartItemList(myShopContext.CHITIETGIOHANGs.ToList()).Where(ci => ci.userid.Equals(userID)).ToList();
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
                dONHANG.TRANGTHAITHANHTOAN = "Chưa thanh toán";
            }
            dONHANG.TONGTIEN = orderModel.Price;
            dONHANG.TRANGTHAIDONHANG = "Chờ lấy hàng";
            dONHANG.GHICHU = orderModel.Note ?? "";
            dONHANG.NGAYSUADOI = DateTime.Now;
            dONHANG.PHIVANCHUYEN = 35000;
            dONHANG.MATTGH = orderModel.Deliveryid;
            myShopContext.Add(dONHANG);
            await myShopContext.SaveChangesAsync();
            THONGTINVANCHUYEN tHONGTINVANCHUYEN = new THONGTINVANCHUYEN();
            tHONGTINVANCHUYEN.MATTGH = orderModel.Deliveryid;
            tHONGTINVANCHUYEN.MADH = dONHANG.MADH;
            myShopContext.Add(tHONGTINVANCHUYEN);
            await myShopContext.SaveChangesAsync();
            if (orderModel.TypePayment == 2)
            {
                THANHTOAN tHANHTOAN = new THANHTOAN();
                tHANHTOAN.MADH = dONHANG.MADH;
                tHANHTOAN.SOTIEN = orderModel.Price;
                tHANHTOAN.NGAYTHANHTOAN = DateTime.Now;
                myShopContext.Add(tHANHTOAN);
                await myShopContext.SaveChangesAsync();
            }
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
            // cartService.ClearCart();
            if (orderModel.TypePayment == 2)
            {
                var url = UrlPayment(orderModel.TypePaymentVN, dONHANG.MADH);
                // code = new { success = true, Code = orderModel.TypePayment, Url = url };
                return Ok(new { success = true, code = orderModel.TypePayment, url = url });
            }
            else return Ok(new { success = true, Code = orderModel.TypePayment, url = "" });
        }

        public async Task<ActionResult> VnpayReturn()
        {
            if (Request.Query.Keys.Count > 0)
            {
                var vnpConfig = configuration.GetSection("VNPAY_SETTINGS");
                string vnp_HashSecret = vnpConfig["vnp_HashSecret"] ?? "";
                var vnpayData = Request.Query.Keys;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string key in vnpayData)
                {
                    // Lấy tất cả dữ liệu chuỗi truy vấn
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, Request.Query[key]);
                    }
                }
                int orderCode = Convert.ToInt32(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.Query["vnp_SecureHash"];
                String TerminalID = Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.Query["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        // var itemOrder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
                        var itemOrder = myShopContext.DONHANGs.Find(orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.TRANGTHAITHANHTOAN = "Đã thanh toán";
                            myShopContext.Update(itemOrder);
                            await myShopContext.SaveChangesAsync();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        ViewBag.PaymentStatus = "Đã thanh toán";
                        ViewBag.OrderID = orderCode;
                        ViewBag.VNPAYID = vnpayTranId;
                        ViewBag.bankCode = bankCode;
                        ViewBag.Result = 1;
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        ViewBag.Result = 0;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
            }
            //var a = UrlPayment(0, "DH3574");
            return View();
        }

        [Route("/checkoutforbuynow", Name = "checkoutforbuynow")]
        public async Task<IActionResult> CheckOutforBuyNow(OrderModel orderModel)
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
            // THONGTINVANCHUYEN tHONGTINVANCHUYEN = new THONGTINVANCHUYEN();
            // tHONGTINVANCHUYEN.MATTGH = orderModel.Deliveryid;
            // tHONGTINVANCHUYEN.MADH = dONHANG.MADH;
            if (orderModel.TypePayment == 2)
            {
                THANHTOAN tHANHTOAN = new THANHTOAN();
                tHANHTOAN.MADH = dONHANG.MADH;
                tHANHTOAN.SOTIEN = orderModel.Price;
                tHANHTOAN.NGAYTHANHTOAN = DateTime.Now;
                myShopContext.Add(tHANHTOAN);
                await myShopContext.SaveChangesAsync();
            }
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
            else return Ok(new { success = true, Code = orderModel.TypePayment, url = "" });
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