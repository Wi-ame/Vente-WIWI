using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vente_en_ligne.Data;
using vente_en_ligne.Models;

namespace vente_en_ligne.Controllers
{
    public class PanierPrincsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanierPrincsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PanierPrincs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PanierPrincs.Include(p => p.Utilisateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PanierPrincs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PanierPrincs == null)
            {
                return NotFound();
            }

            var panierPrinc = await _context.PanierPrincs
                .Include(p => p.Utilisateur)
                .FirstOrDefaultAsync(m => m.PID == id);
            if (panierPrinc == null)
            {
                return NotFound();
            }

            return View(panierPrinc);
        }

        // GET: PanierPrincs/Create
        public IActionResult Create()
        {
            ViewData["IDU"] = new SelectList(_context.Utilisateurs, "ID", "ID");
            return View();
        }

        // POST: PanierPrincs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PID,IDU,DateCréation")] PanierPrinc panierPrinc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panierPrinc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDU"] = new SelectList(_context.Utilisateurs, "ID", "ID", panierPrinc.IDU);
            return View(panierPrinc);
        }

        // GET: PanierPrincs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PanierPrincs == null)
            {
                return NotFound();
            }

            var panierPrinc = await _context.PanierPrincs.FindAsync(id);
            if (panierPrinc == null)
            {
                return NotFound();
            }
            ViewData["IDU"] = new SelectList(_context.Utilisateurs, "ID", "ID", panierPrinc.IDU);
            return View(panierPrinc);
        }

        // POST: PanierPrincs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PID,IDU,DateCréation")] PanierPrinc panierPrinc)
        {
            if (id != panierPrinc.PID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panierPrinc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierPrincExists(panierPrinc.PID))
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
            ViewData["IDU"] = new SelectList(_context.Utilisateurs, "ID", "ID", panierPrinc.IDU);
            return View(panierPrinc);
        }

        // GET: PanierPrincs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PanierPrincs == null)
            {
                return NotFound();
            }

            var panierPrinc = await _context.PanierPrincs
                .Include(p => p.Utilisateur)
                .FirstOrDefaultAsync(m => m.PID == id);
            if (panierPrinc == null)
            {
                return NotFound();
            }

            return View(panierPrinc);
        }

        // POST: PanierPrincs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PanierPrincs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PanierPrincs'  is null.");
            }
            var panierPrinc = await _context.PanierPrincs.FindAsync(id);
            if (panierPrinc != null)
            {
                _context.PanierPrincs.Remove(panierPrinc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierPrincExists(int id)
        {
          return (_context.PanierPrincs?.Any(e => e.PID == id)).GetValueOrDefault();
        }
    }
}
