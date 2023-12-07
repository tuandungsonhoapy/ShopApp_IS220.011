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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly MyShopContext _context;

        public SanPhamController(MyShopContext context)
        {
            _context = context;
        }

        // GET: SanPham
        [HttpGet("/admin/quanlysanpham")]
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize, string SearchString)
        {
            IQueryable<SANPHAM> myShopContext;
            IEnumerable<SANPHAM> productsInPage;
            IEnumerable<SANPHAM> productsSearch;

            int totalProdut;

            if (!string.IsNullOrEmpty(SearchString))
            {
                productsSearch = await _context.SANPHAMs
                                       .Where(sp => sp.TENSP.Contains(SearchString) || sp.PL_SP.TENPL
                                       .Contains(SearchString)).ToListAsync();
                totalProdut = productsSearch.Count();
                if (pagesize <= 0) { pagesize = 10; }
                int countPages = (int)Math.Ceiling((double)totalProdut / pagesize);


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
                            SearchString = SearchString
                        });
                        return v;
                    }
                };
                ViewData["currentpage_product"] = currentPage;
                ViewData["pagesize_product"] = pagesize;

                ViewBag.pagingmodel = pagingmodel;
                ViewBag.totalProdut = totalProdut;

                productsInPage = await _context.SANPHAMs
                        .Where(sp => sp.TENSP.Contains(SearchString) || sp.PL_SP.TENPL.Contains(SearchString) || sp.PLTHOITRANG.Contains(SearchString))
                        .Skip((currentPage - 1) * pagesize)
                        .Take(pagesize)
                        .Include(p => p.PL_SP)
                        .ToListAsync();

            }
            else
            {
                totalProdut = await _context.SANPHAMs.CountAsync();
                if (pagesize <= 0) { pagesize = 10; }
                int countPages = (int)Math.Ceiling((double)totalProdut / pagesize);


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
                            seachstring = SearchString
                        });
                        return v;
                    }
                };
                ViewData["currentpage_product"] = currentPage;
                ViewData["pagesize_product"] = pagesize;

                ViewBag.pagingmodel = pagingmodel;
                ViewBag.totalProdut = totalProdut;


                productsInPage = await _context.SANPHAMs.Skip((currentPage - 1) * pagesize)
                        .Take(pagesize)
                        .Include(p => p.PL_SP)
                        .ToListAsync();
            }




            // if (!string.IsNullOrEmpty(SearchString))
            // {
            //     myShopContext = _context.SANPHAMs
            //     .Include(s => s.PL_SP)
            //     .Where(sp => sp.TENSP.Contains(SearchString) || sp.PL_SP.TENPL.Contains(SearchString));
            // }
            // else
            // {
            //     myShopContext = _context.SANPHAMs.Include(s => s.PL_SP);
            // }

            return View(productsInPage);
        }

        // GET: SanPham/Details/5
        [HttpGet("/admin/sanpham/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SANPHAMs == null)
            {
                return NotFound();
            }

            var sANPHAM = await _context.SANPHAMs
                .Include(s => s.PL_SP)
                .FirstOrDefaultAsync(m => m.MASP == id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            return View(sANPHAM);
        }


        // GET: SanPham/Create
        [HttpGet("/admin/sanpham/create/")]
        public IActionResult Create()
        {
            ViewData["MAPL"] = new SelectList(_context.PL_SPs, "MAPL", "TENPL");
            //ViewBag.PL_SPs = new SelectList(_context.PL_SPs, "MAPL", "TENPL");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/admin/sanpham/create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TENSP,GIAGOC,MAPL,MOTA,GIABAN,PLTHOITRANG,MainImg")] SANPHAM sANPHAM)
        {
            //ViewBag.PL_SPs = new SelectList(_context.PL_SPs, "MAPL", "TENPL", sANPHAM.MAPL);
            ViewData["MAPL"] = new SelectList(_context.PL_SPs, "MAPL", "TENPL", sANPHAM.MAPL);

            if (ModelState.IsValid)
            {
                _context.Add(sANPHAM);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Thêm sản phẩm mới thành công";
                return RedirectToAction("Index", "SanPham");
            }
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    TempData["StatusDangerMessage"] = "Thêm sản phẩm mới thất bại. Lỗi: " + error.ErrorMessage.ToString();
                }
            }
            return View(sANPHAM);
        }

        // GET: SanPham/Create
        [HttpGet]
        public async Task<IActionResult> CreateImg(int id)
        {
            if (_context.SANPHAMs == null)
            {
                return NotFound();
            }
            var product = await _context.SANPHAMs.FirstOrDefaultAsync(pro => pro.MASP == id);
            if (product != null)
            {
                ViewBag.Product = product;
            }
            else
            {
                return NotFound();
            }

            int defaultMASPValue = id;
            ViewBag.DefaultMASPValue = defaultMASPValue;
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImg([Bind("TENHINHANH, MASP, LINK")] HINHANH hINHANH)
        {
            // if (LINK != null && LINK.Length > 0)
            // {
            //     var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Img/Product");

            //     // Tạo thư mục "Uploads" nếu nó chưa tồn tại
            //     if (!Directory.Exists(uploadsFolder))
            //     {
            //         Directory.CreateDirectory(uploadsFolder);
            //     }

            //     // Tạo tên file duy nhất để tránh trùng lặp
            //     var uniqueFileName = Guid.NewGuid().ToString() + "_" + LINK.FileName;
            //     var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //     using (var fileStream = new FileStream(filePath, FileMode.Create))
            //     {
            //         LINK.CopyTo(fileStream);
            //     }
            //     TempData["StatusMessage"] = "abc";
            //     hINHANH.LINK = uniqueFileName;
            // }
            if (ModelState.IsValid)
            {

                _context.Add(hINHANH);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Thêm hình ảnh mới thành công";
                return RedirectToAction(nameof(Index));
            }
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    TempData["StatusDangerMessage"] = "Thêm sản phẩm mới thất bại. Lỗi: " + error.ErrorMessage.ToString();
                }
            }
            return View(hINHANH);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSample(int id)
        {
            if (_context.SANPHAMs == null)
            {
                return NotFound();
            }
            var product = await _context.SANPHAMs.FirstOrDefaultAsync(pro => pro.MASP == id);
            if (product != null)
            {
                ViewBag.Product = product;
            }
            else
            {
                return NotFound();
            }

            ViewData["MAMAU"] = new SelectList(_context.MAUSACs, "MAMAU", "TENMAU");
            ViewData["MASIZE"] = new SelectList(_context.SIZEs, "MASIZE", "Size");

            int defaultMASPValue = id;
            ViewBag.DefaultMASPValue = defaultMASPValue;
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSample([Bind("MAMAU,MASP,SOLUONG,MASIZE")] CHITIETSANPHAM cHITIETSANPHAM, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {

                _context.Add(cHITIETSANPHAM);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Thêm mới mẫu sản phẩm thành công";
                // Kiểm tra xem có tham số returnUrl trong yêu cầu không
                // if (string.IsNullOrEmpty(returnUrl))
                // {
                //     // Nếu không, lấy địa chỉ URL trước đó từ Header Referer
                //     returnUrl = Request.Headers["Referer"];
                //     TempData["StatusDangerMessage"] = returnUrl.ToString();
                // }

                // // Kiểm tra xem returnUrl có giá trị và là một URL cục bộ hợp lệ không
                // if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                // {
                //     return Redirect(returnUrl);
                // }
                return RedirectToAction(nameof(Index));
            }
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    TempData["StatusDangerMessage"] = "Thêm sản phẩm mới thất bại. Lỗi: " + error.ErrorMessage.ToString();
                }
            }
            return View(cHITIETSANPHAM);
        }

        // GET: SanPham/Edit/5
        [HttpGet("/admin/sanpham/edit/{id}")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SANPHAMs == null)
            {
                return NotFound();
            }

            var sANPHAM = await _context.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return NotFound();
            }
            ViewData["MAPL"] = new SelectList(_context.PL_SPs, "MAPL", "TENPL", sANPHAM.MAPL);
            return View(sANPHAM);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/admin/sanpham/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MASP,TENSP,GIAGOC,MAPL,MOTA,GIABAN,PLTHOITRANG,MainImg")] SANPHAM sANPHAM)
        {
            if (id != sANPHAM.MASP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sANPHAM);
                    await _context.SaveChangesAsync();
                    TempData["StatusMessage"] = "Chỉnh sửa thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SANPHAMExists(sANPHAM.MASP))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MAPL"] = new SelectList(_context.PL_SPs, "MAPL", "TENPL", sANPHAM.MAPL);
            return View(sANPHAM);
        }

        // GET: SanPham/Delete/5
        [HttpGet("/admin/sanpham/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SANPHAMs == null)
            {
                return NotFound();
            }

            var sANPHAM = await _context.SANPHAMs
                .Include(s => s.PL_SP)
                .FirstOrDefaultAsync(m => m.MASP == id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            return View(sANPHAM);
        }

        // POST: SanPham/Delete/5
        [HttpPost("/admin/sanpham/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SANPHAMs == null)
            {
                return Problem("Entity set 'MyShopContext.SANPHAMs'  is null.");
            }
            var sANPHAM = await _context.SANPHAMs.FindAsync(id);
            if (sANPHAM != null)
            {
                _context.SANPHAMs.Remove(sANPHAM);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SANPHAMExists(int id)
        {
            return (_context.SANPHAMs?.Any(e => e.MASP == id)).GetValueOrDefault();
        }

        public class UploadFile{
            [Required]
            [DataType(DataType.Upload)]
            [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
            [Display(Name = "Chọn file ảnh sản phẩm")]
            public IFormFile FileUpload {set;get;}
        }

        [HttpGet]
        public IActionResult UploadPhoto(int id)
        {
            var product = _context.SANPHAMs.Where(sp => sp.MASP == id)
                                           .Include(sp => sp.Image)
                                           .FirstOrDefault();
            if(product == null)
            {
                return NotFound("Không có sản phẩm!");
            }
            ViewBag.Product = product;
            return View(new UploadFile());
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhotoAsync(int id, [Bind("FileUpload")] UploadFile file)
        {
            var product = _context.SANPHAMs.Find(id);
            if(product == null)
            {
                return NotFound("Không có sản phẩm!");
            }
            ViewBag.Product = product;

            if(file != null)
            {
                var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileUpload.FileName);

                var f = Path.Combine("Uploads/Img/Product", file1);

                using (var filestream = new FileStream(f, FileMode.Create))
                {
                    await file.FileUpload.CopyToAsync(filestream);
                }

                _context.Add(new HINHANH(){
                    MASP = product.MASP,
                    TENHINHANH = file1,
                    LINK = file1
                });

                await _context.SaveChangesAsync();

            }
            return View(new UploadFile());
        }
    }
}
