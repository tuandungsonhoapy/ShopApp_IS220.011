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
    public class MauSacController : Controller
    {
        private readonly MyShopContext _context;

        public MauSacController(MyShopContext context)
        {
            _context = context;
        }

        // GET: MauSac
        public async Task<IActionResult> Index()
        {
              return _context.MAUSACs != null ? 
                          View(await _context.MAUSACs.ToListAsync()) :
                          Problem("Entity set 'MyShopContext.MAUSACs'  is null.");
        }

        // GET: MauSac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MAUSACs == null)
            {
                return NotFound();
            }

            var mAUSAC = await _context.MAUSACs
                .FirstOrDefaultAsync(m => m.MAMAU == id);
            if (mAUSAC == null)
            {
                return NotFound();
            }

            return View(mAUSAC);
        }

        // GET: MauSac/Create
        [HttpGet("mausac/create/")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MauSac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("mausac/create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TENMAU,HEX")] MAUSAC mAUSAC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mAUSAC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mAUSAC);
        }

        // GET: MauSac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MAUSACs == null)
            {
                return NotFound();
            }

            var mAUSAC = await _context.MAUSACs.FindAsync(id);
            if (mAUSAC == null)
            {
                return NotFound();
            }
            return View(mAUSAC);
        }

        // POST: MauSac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MAMAU,TENMAU,HEX")] MAUSAC mAUSAC)
        {
            if (id != mAUSAC.MAMAU)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mAUSAC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MAUSACExists(mAUSAC.MAMAU))
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
            return View(mAUSAC);
        }

        // GET: MauSac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MAUSACs == null)
            {
                return NotFound();
            }

            var mAUSAC = await _context.MAUSACs
                .FirstOrDefaultAsync(m => m.MAMAU == id);
            if (mAUSAC == null)
            {
                return NotFound();
            }

            return View(mAUSAC);
        }

        // POST: MauSac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MAUSACs == null)
            {
                return Problem("Entity set 'MyShopContext.MAUSACs'  is null.");
            }
            var mAUSAC = await _context.MAUSACs.FindAsync(id);
            if (mAUSAC != null)
            {
                _context.MAUSACs.Remove(mAUSAC);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MAUSACExists(int id)
        {
          return (_context.MAUSACs?.Any(e => e.MAMAU == id)).GetValueOrDefault();
        }
    }
}
