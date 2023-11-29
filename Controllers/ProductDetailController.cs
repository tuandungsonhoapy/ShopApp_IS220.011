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
            return View(product);
        }

        /// Thêm sản phẩm vào cart
        // [Route("addcart/{productid:int}/", Name = "addcart")]

        [HttpPost]
        [Route("/addcart", Name = "addcart")]
        public IActionResult AddToCart([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity)
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
                              TENSP = p.TENSP,
                              GIABAN = p.GIABAN,
                              GIAGOC = p.GIAGOC,
                              MAMAU = color.MAMAU,
                              TENMAU = color.TENMAU,
                              HEX = color.HEX,
                              MASIZE = size.MASIZE,
                              Size = size.Size,
                              SoLuong = quantity,
                              MainImg = p.MainImg,
                          })
                          .FirstOrDefault();

            // var productdetail = (from pro in myShopContext.SANPHAMs
            //                     where pro.MASP == productid
            //                     join prodetail in myShopContext.CHITIETSANPHAMs
            //                     on pro.MASP equals prodetail.MASP
            //                     where prodetail.MAMAU == colorid && prodetail.MASIZE == sizeid
            //                     select new ProductDetailModel()
            //                     {
            //                         MASP = pro.MASP,
            //                         TENSP = pro.TENSP,
            //                         MACTSP = prodetail.MACTSP,
            //                         MAMAU = prodetail.MAMAU,
            //                         HEX = prodetail.MAUSAC.HEX,

            //                     })
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
            var cart = cartService.GetCartItems();
            var cart_list = new List<CartItem>();
            cart_list = cart.Where(p => p.userid.Equals(userID)).ToList();
            return View(cart_list);
        }

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
        public IActionResult Increase([FromForm] int productid, [FromForm] int colorid, [FromForm] int sizeid, [FromForm] int quantity)
        {
            var user = userManager.GetUserAsync(User).Result;
            var userID = "null";
            if (user != null)
            {
                userID = user.Id;
            }
            var cart = cartService.GetCartItems();
            var cartItem = cart.Where(c => c.product.MASP == productid && c.product.MAMAU == colorid && c.product.MASIZE == sizeid && c.userid.Equals(userID)).FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.quantity = quantity;
            }
            else
            {
                TempData["StatusDangerMessage"] = "cartitem null";
            }
            cartService.SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            var updatedCartHtml = this.RenderPartialViewToString("_Cart", cart);
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

