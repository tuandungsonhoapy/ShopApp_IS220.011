using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DBModels;
using App.Areas.Identity.Models.ManageViewModels;
using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Editor + "," + RoleName.Administrator)]
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly MyShopContext _context;

        public SizeController(MyShopContext context)
        {
            _context = context;
        }

        // GET: MauSac
        public async Task<IActionResult> Index()
        {
              return _context.SIZEs != null ? 
                          View(await _context.SIZEs.ToListAsync()) :
                          Problem("Entity set 'MyShopContext.SIZEs'  is null.");
        }

        // GET: MauSac/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MauSac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Size")] SIZE sIZEnew)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sIZEnew);
                await _context.SaveChangesAsync();
                TempData["StatusMess"] = "Thêm mới size thành công";
                return RedirectToAction(nameof(Index));
            }
            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    TempData["StatusDangerMessage"] = "Thêm mới size thất bại. Lỗi: " + error.ErrorMessage.ToString();
                }
            }
            return View(sIZEnew);
        }

        // GET: MauSac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SIZEs == null)
            {
                return NotFound();
            }

            var mSIZE = await _context.SIZEs
                .FirstOrDefaultAsync(m => m.MASIZE == id);
            if (mSIZE == null)
            {
                return NotFound();
            }

            return View(mSIZE);
        }

        // POST: MauSac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SIZEs == null)
            {
                return Problem("Entity set 'MyShopContext.SIZEs'  is null.");
            }
            var mSIZE = await _context.SIZEs.FindAsync(id);
            if (mSIZE != null)
            {
                _context.SIZEs.Remove(mSIZE);
            }
            
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Xóa size thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool MAUSACExists(int id)
        {
          return (_context.SIZEs?.Any(e => e.MASIZE == id)).GetValueOrDefault();
        }
    }
}