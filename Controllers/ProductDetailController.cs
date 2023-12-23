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

namespace DAFW_IS220.Controllers
{
    public static class ControllerExtensions
    {
        public static string RenderPartialViewToString(this ProductDetailController controller, string viewName, object model)
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
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;

        private readonly MyShopContext myShopContext;

        private readonly CartService cartService;

        private readonly UserManager<AppUser> userManager;

        public ProductDetailController(ILogger<ProductDetailController> logger, MyShopContext _myshopcontext, CartService _carservice, UserManager<AppUser> user)
        {
            _logger = logger;
            myShopContext = _myshopcontext;
            cartService = _carservice;
            userManager = user;
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

        [AllowAnonymous]
        //[Route("chitietsanpham")]
        //[Route("chitietsanpham/{id:int?})]
        //[Route("chitietsanpham/[controller]/[action])]
        //[Route("[controller]-[action].html")]
        public IActionResult Index(int id)
        {
            var product = myShopContext.SANPHAMs.Find(id);
            var productdetais = myShopContext.CHITIETSANPHAMs.Where(p => p.MASP == id)
                                                                 .Include(p => p.MAUSAC)
                                                                 .Include(p => p.SIZE)
                                                                 .ToList();
            ViewBag.DanhGia = myShopContext.DANHGIAs.Where(p => p.MASP == id);
            // ViewBag.ProductDetail = myShopContext.CHITIETSANPHAMs.Where(p => p.MASP == id)
            //                                                      .Include(p => p.MAUSAC)
            //                                                      .Include(p => p.SIZE)
            //                                                      .ToList();
            // foreach(var item in productdetais)
            // {
            //     var e = myShopContext.Entry(item);
            //     e.Reference(p => p.MAUSAC);
            //     e.Reference(p => p.SIZE);
            // }
            ViewBag.ProductDetail = (from pd in productdetais
                                     join color in myShopContext.MAUSACs
                                     on pd.MAMAU equals color.MAMAU
                                     join size in myShopContext.SIZEs
                                     on pd.MASIZE equals size.MASIZE
                                     select new ProductDetailModel()
                                     {
                                         MASP = pd.MASP,
                                         MACTSP = pd.MACTSP,
                                         MAMAU = pd.MAMAU,
                                         MASIZE = pd.MASIZE,
                                         HEX = color.HEX,
                                         TENMAU = color.TENMAU,
                                         Size = size.Size,
                                         SoLuong = pd.SOLUONG
                                     }).ToList();
            ViewBag.Img = myShopContext.HINHANHs.Where(h => h.MASP == id);
            ViewBag.otherProduct = (from pro in myShopContext.SANPHAMs
                                    where pro.MAPL.Equals(product.MAPL) && pro.PLTHOITRANG.Equals(product.PLTHOITRANG) && pro.MASP != id
                                    join urlanh in myShopContext.HINHANHs
                                    on pro.MASP equals urlanh.MASP
                                    select new SANPHAMModel()
                                    {
                                        MASP = pro.MASP,
                                        TENSP = pro.TENSP,
                                        GIABAN = pro.GIABAN,
                                        GIAGOC = pro.GIAGOC,
                                        HINHANH = urlanh.LINK
                                    })
                                    .GroupBy(p => p.MASP)
                                    .Select(g => g.First())
                                    .ToList();
            ViewBag.FeedBacks = (from fb in myShopContext.DANHGIAs
                                 join user in myShopContext.Users
                                 on fb.MATK equals user.Id
                                 where fb.MASP == id
                                 select new DANHGIA()
                                 {
                                     MADANHGIA = fb.MADANHGIA,
                                     MATK = fb.MATK,
                                     TAIKHOAN = user,
                                     MASP = fb.MASP,
                                     MADH = fb.MADH,
                                     SOSAO = fb.SOSAO,
                                     NOIDUNG = fb.NOIDUNG
                                 }).ToList();
            return View(product);
        }

        /// Thêm sản phẩm vào cart
        // [Route("addcart/{productid:int}/", Name = "addcart")]

        [HttpPost]
        [Route("/addcart", Name = "addcart")]
        public IActionResult AddToCart([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity, [FromForm] int productdetailid)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            // Lấy đối tượng HttpContext từ Controller
            var httpContext = HttpContext;

            // Lấy thông tin về request và URL
            var request = httpContext.Request;
            var url = request.Path + request.QueryString;
            TempData["StatusDangerMessage"] = colorid + " - " + sizeid + " - " + quantity;
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

            // Xử lý đưa vào Cart ...
            var cart = cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.MASP == productid);
            var car_item = cart.Where(p => p.product.MASP == productid && p.product.MAMAU == colorid && p.product.MASIZE == sizeid && p.userid.Equals(userID)).FirstOrDefault();
            if (car_item != null)
            {
                // Đã tồn tại, tăng thêm 1
                car_item.quantity += product.SoLuong;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = product.SoLuong, product = product, userid = userID });
            }

            // Lưu cart vào Session
            cartService.SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Route("/addproducttocart", Name = "addproducttocart")]
        public async Task<IActionResult> AddProductToCart([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity, [FromForm] int productdetailid)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            // Xử lý đưa vào cart
            var cartitem = (from cartdetail in myShopContext.CHITIETGIOHANGs
                            join productdetail in myShopContext.CHITIETSANPHAMs
                            on cartdetail.MACTSP equals productdetail.MACTSP
                            where cartdetail.MASP == productid && productdetail.MAMAU == colorid && productdetail.MASIZE == sizeid && cartdetail.MATK.Equals(userID)
                            select cartdetail).FirstOrDefault();
            var productdetailID = myShopContext.CHITIETSANPHAMs.Where(pd => pd.MASP == productid && pd.MAMAU == colorid && pd.MASIZE == sizeid).FirstOrDefault();
            if (cartitem != null)
            {
                cartitem.SOLUONGMUA += quantity;
                myShopContext.Update(cartitem);
                await myShopContext.SaveChangesAsync();
            }
            else
            {
                var product = myShopContext.SANPHAMs.Find(productid);
                CHITIETGIOHANG cHITIETGIOHANG = new CHITIETGIOHANG();
                cHITIETGIOHANG.MATK = userID;
                cHITIETGIOHANG.MASP = productid;
                if (productdetailID != null) cHITIETGIOHANG.MACTSP = productdetailID.MACTSP;
                cHITIETGIOHANG.SOLUONGMUA = quantity;
                if (product != null) cHITIETGIOHANG.TONGGIA = product.GIABAN * quantity;
                myShopContext.Add(cHITIETGIOHANG);
                await myShopContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Cart));
        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {

            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            // var cart = cartService.GetCartItems();
            // var cart_list = new List<CartItem>();
            // cart_list = cart.Where(p => p.userid.Equals(userID)).ToList();

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
            return View(cartList);
        }

        // [Route("/cart", Name = "cart")]
        // public IActionResult Cart()
        // {
        //     var user = userManager.GetUserAsync(User).Result;
        //     var userID = "null";
        //     if (user != null)
        //     {
        //         userID = user.Id;
        //     }
        //     var cart = cartService.GetCartItems();
        //     var cart_list = new List<CartItem>();
        //     cart_list = cart.Where(p => p.userid.Equals(userID)).ToList();
        //     return View(cart_list);
        // }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var cart = cartService.GetCartItems();
            var cartitem = cart.Where(p => p.product.MASP == productid && p.userid.Equals(userID)).FirstOrDefault();
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        [Route("/removecartitem/{productid:int}", Name = "removecartitem")]
        public async Task<IActionResult> RemoveCartItem([FromRoute] int productid)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var cartItem = myShopContext.CHITIETGIOHANGs.Where(dc => dc.MASP == productid && dc.MATK.Equals(userID)).FirstOrDefault();
            if (cartItem != null)
            {
                myShopContext.Remove(cartItem);
                await myShopContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Cart));
        }


        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = cartService.GetCartItems();

            // var cartitem = cart.Find(p => p.product.MASP == productid && p.product.MAMAU == colorid && p.product.MASIZE == sizeid);
            var cartitem = cart.Where(p => p.product.MASP == productid && p.product.MAMAU == colorid && p.product.MASIZE == sizeid).FirstOrDefault();
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            else
            {
                TempData["StatusDangerMessage"] = "cartitem null";
            }
            cartService.SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        [HttpPost]
        [Route("/changequantity", Name = "changequantity")]
        public async Task<IActionResult> Increase([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            // var cart = cartService.GetCartItems();
            // var cart_list = cart.Where(c => c.userid.Equals(userID)).ToList();
            // var cartItem = cart_list.Where(c => c.product.MASP == productid && c.product.MAMAU == colorid && c.product.MASIZE == sizeid).FirstOrDefault();

            var cart_list = GetCartItemList(myShopContext.CHITIETGIOHANGs.ToList()).Where(dc => dc.userid.Equals(userID)).ToList();
            var cartItem = cart_list.Where(c => c.product.MASP == productid && c.product.MAMAU == colorid && c.product.MASIZE == sizeid).FirstOrDefault();
            if (cartItem != null)
            {
                var cart_item = myShopContext.CHITIETGIOHANGs.Where(dc => dc.MASP == productid && dc.MACTSP == cartItem.product.MACTSP && dc.MATK.Equals(userID)).FirstOrDefault();
                if (cart_item != null)
                {
                    cart_item.SOLUONGMUA = quantity;
                    cart_item.TONGGIA = quantity * cartItem.product.GIABAN;
                    myShopContext.Update(cart_item);
                    await myShopContext.SaveChangesAsync();
                }
            }
            else
            {
                TempData["StatusDangerMessage"] = "cartitem null";
            }
            // cartService.SaveCartSession(cart);
            var cart_list_new = GetCartItemList(myShopContext.CHITIETGIOHANGs.ToList()).Where(dc => dc.userid.Equals(userID)).ToList();
            var updatedCartHtml = this.RenderPartialViewToString("_Cart", cart_list_new);
            return Content(updatedCartHtml);

            // if (cart.Count == 0)
            // {
            //     HttpContext.Session.Remove("Cart");
            // }
            // else
            // {
            //     HttpContext.Session.SetJson("Cart", cart);
            // }
            // TempData["StatusMessage"] = "Increase Item Quantity to Cart Successfully";
            // return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

