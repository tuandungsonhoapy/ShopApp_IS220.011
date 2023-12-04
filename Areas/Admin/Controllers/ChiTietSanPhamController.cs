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
using Org.BouncyCastle.Crypto.Prng.Drbg;


namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class ChiTietSanPhamController : Controller
    {
        private readonly MyShopContext _context;

        public ChiTietSanPhamController(MyShopContext context)
        {
            _context = context;
        }

        // GET: ChiTietSanPham
        public async Task<IActionResult> Index()
        {
            var myShopContext = _context.CHITIETSANPHAMs.Include(c => c.MAUSAC).Include(c => c.SANPHAM).Include(c => c.SIZE).OrderBy(c => c.MASP);
            return View(await myShopContext.ToListAsync());
        }

        // GET: ChiTietSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CHITIETSANPHAMs == null)
            {
                return NotFound();
            }

            var cHITIETSANPHAM = await _context.CHITIETSANPHAMs
                .Include(c => c.MAUSAC)
                .Include(c => c.SANPHAM)
                .FirstOrDefaultAsync(m => m.MACTSP == id);
            if (cHITIETSANPHAM == null)
            {
                return NotFound();
            }

            return View(cHITIETSANPHAM);
        }

        // GET: ChiTietSanPham/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MAMAU"] = new SelectList(_context.MAUSACs, "MAMAU", "TENMAU");
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP");
            ViewData["MASIZE"] = new SelectList(_context.SIZEs, "MASIZE", "Size");
            return View();
        }

        // POST: ChiTietSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MAMAU,MASP,SOLUONG,MASIZE")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            ViewData["MAMAU"] = new SelectList(_context.MAUSACs, "MAMAU", "TENMAU",cHITIETSANPHAM.MAMAU);
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", cHITIETSANPHAM.MASP);
            ViewData["MASIZE"] = new SelectList(_context.SIZEs, "MASIZE", "Size", cHITIETSANPHAM.MASIZE);
            if (ModelState.IsValid)
            {
                _context.Add(cHITIETSANPHAM);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Thêm mẫu sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(cHITIETSANPHAM);
        }

        // GET: ChiTietSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CHITIETSANPHAMs == null)
            {
                return NotFound();
            }

            var cHITIETSANPHAM = await _context.CHITIETSANPHAMs.FindAsync(id);
            if (cHITIETSANPHAM == null)
            {
                return NotFound();
            }
            ViewData["MAMAU"] = new SelectList(_context.MAUSACs, "MAMAU", "TENMAU", cHITIETSANPHAM.MAMAU);
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", cHITIETSANPHAM.MASP);
            ViewData["MASIZE"] = new SelectList(_context.SIZEs, "MASIZE", "Size", cHITIETSANPHAM.MASIZE);
            return View(cHITIETSANPHAM);
        }

        // POST: ChiTietSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MACTSP,MAMAU,MASIZE,MASP,SOLUONG")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            if (id != cHITIETSANPHAM.MACTSP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cHITIETSANPHAM);
                    await _context.SaveChangesAsync();
                    TempData["StatusMessage"] = "Cập nhật mẫu sản phẩm thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHITIETSANPHAMExists(cHITIETSANPHAM.MACTSP))
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
            ViewData["MAMAU"] = new SelectList(_context.MAUSACs, "MAMAU", "TENMAU", cHITIETSANPHAM.MAMAU);
            ViewData["MASP"] = new SelectList(_context.SANPHAMs, "MASP", "TENSP", cHITIETSANPHAM.MASP);
            ViewData["MASIZE"] = new SelectList(_context.SIZEs, "MASIZE", "Size", cHITIETSANPHAM.MASIZE);
            return View(cHITIETSANPHAM);
        }

        // GET: ChiTietSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CHITIETSANPHAMs == null)
            {
                return NotFound();
            }

            var cHITIETSANPHAM = await _context.CHITIETSANPHAMs
                .Include(c => c.MAUSAC)
                .Include(c => c.SANPHAM)
                .Include(c => c.SIZE)
                .FirstOrDefaultAsync(m => m.MACTSP == id);

            if (cHITIETSANPHAM == null)
            {
                return NotFound();
            }

            return View(cHITIETSANPHAM);
        }

        // POST: ChiTietSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CHITIETSANPHAMs == null)
            {
                return Problem("Entity set 'MyShopContext.CHITIETSANPHAMs'  is null.");
            }
            var cHITIETSANPHAM = await _context.CHITIETSANPHAMs.FindAsync(id);
            if (cHITIETSANPHAM != null)
            {
                _context.CHITIETSANPHAMs.Remove(cHITIETSANPHAM);
            }
            
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Xóa mẫu sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool CHITIETSANPHAMExists(int id)
        {
          return (_context.CHITIETSANPHAMs?.Any(e => e.MACTSP == id)).GetValueOrDefault();
        }
    }
}
