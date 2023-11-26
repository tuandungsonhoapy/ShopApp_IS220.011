using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.DBModels;
using App.Areas.Identity.Models.ManageViewModels;
using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class HinhAnhController : Controller
    {
        private readonly MyShopContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public HinhAnhController(MyShopContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: HinhAnhs
        public async Task<IActionResult> Index()
        {
            var hinhanhs = await _context.HINHANHs.Include(h => h.SANPHAM).ToListAsync();
            return View(hinhanhs);
        }

        // GET: HinhAnhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HINHANHs == null)
            {
                return NotFound();
            }

            var hINHANHs = await _context.HINHANHs
                .Include(h => h.SANPHAM)
                .FirstOrDefaultAsync(m => m.MAHINHANH == id);
            if (hINHANHs == null)
            {
                return NotFound();
            }

            return View(hINHANHs);
        }

        // GET: HinhAnhs/Create
        [HttpGet("/hinhanh/create/")]
        public IActionResult Create()
        {
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP");
            return View();
        }

        // POST: HinhAnhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/hinhanh/create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TENHINHANH, MASP, LINK")] HINHANH hINHANH)
        {
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", hINHANH.MASP);
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

        // GET: HinhAnhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HINHANHs == null)
            {
                return NotFound();
            }

            var hINHANHs = await _context.HINHANHs.FindAsync(id);
            if (hINHANHs == null)
            {
                return NotFound();
            }
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", hINHANHs.MASP);
            return View(hINHANHs);
        }

        // POST: HinhAnhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MAHINHANH,TENHINHANH,MASP,LINK")] HINHANH hINHANH)
        {
            if (id != hINHANH.MAHINHANH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hINHANH);
                    await _context.SaveChangesAsync();
                    TempData["StatusMessage"] = "Chỉnh sửa hình ảnh thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HINHANHsExists(hINHANH.MAHINHANH))
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
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", hINHANH.MASP);
            return View(hINHANH);
        }

        // GET: HinhAnhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HINHANHs == null)
            {
                return NotFound();
            }

            var hINHANHs = await _context.HINHANHs
                .Include(h => h.SANPHAM)
                .FirstOrDefaultAsync(m => m.MAHINHANH == id);
            if (hINHANHs == null)
            {
                return NotFound();
            }

            return View(hINHANHs);
        }

        // POST: HinhAnhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HINHANHs == null)
            {
                return Problem("Entity set 'MyShopContext.HINHANHs'  is null.");
            }
            var hINHANHs = await _context.HINHANHs.FindAsync(id);
            if (hINHANHs != null)
            {
                _context.HINHANHs.Remove(hINHANHs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HINHANHsExists(int id)
        {
            return (_context.HINHANHs?.Any(e => e.MAHINHANH == id)).GetValueOrDefault();
        }
    }
}
