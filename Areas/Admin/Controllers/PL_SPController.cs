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
    public class PL_SPController : Controller
    {
        private readonly MyShopContext _context;

        public PL_SPController(MyShopContext context)
        {
            _context = context;
        }

        // GET: PL_SP
        public async Task<IActionResult> Index()
        {
              return _context.PL_SPs != null ? 
                          View(await _context.PL_SPs.ToListAsync()) :
                          Problem("Entity set 'MyShopContext.PL_SPs'  is null.");
        }

        // GET: PL_SP/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PL_SPs == null)
            {
                return NotFound();
            }

            var pL_SP = await _context.PL_SPs
                .FirstOrDefaultAsync(m => m.MAPL == id);
            if (pL_SP == null)
            {
                return NotFound();
            }

            return View(pL_SP);
        }

        // GET: PL_SP/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PL_SP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MAPL,TENPL")] PL_SP pL_SP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pL_SP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pL_SP);
        }

        // GET: PL_SP/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PL_SPs == null)
            {
                return NotFound();
            }

            var pL_SP = await _context.PL_SPs.FindAsync(id);
            if (pL_SP == null)
            {
                return NotFound();
            }
            return View(pL_SP);
        }

        // POST: PL_SP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MAPL,TENPL")] PL_SP pL_SP)
        {
            if (id != pL_SP.MAPL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pL_SP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PL_SPExists(pL_SP.MAPL))
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
            return View(pL_SP);
        }

        // GET: PL_SP/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PL_SPs == null)
            {
                return NotFound();
            }

            var pL_SP = await _context.PL_SPs
                .FirstOrDefaultAsync(m => m.MAPL == id);
            if (pL_SP == null)
            {
                return NotFound();
            }

            return View(pL_SP);
        }

        // POST: PL_SP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PL_SPs == null)
            {
                return Problem("Entity set 'MyShopContext.PL_SPs'  is null.");
            }
            var pL_SP = await _context.PL_SPs.FindAsync(id);
            if (pL_SP != null)
            {
                _context.PL_SPs.Remove(pL_SP);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PL_SPExists(int id)
        {
          return (_context.PL_SPs?.Any(e => e.MAPL == id)).GetValueOrDefault();
        }
    }
}
