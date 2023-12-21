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
    public class PaniersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaniersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paniers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Panier.Include(p => p.PanierPrinc).Include(p => p.produit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Paniers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .Include(p => p.PanierPrinc)
                .Include(p => p.produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // GET: Paniers/Create
        public IActionResult Create()
        {
            ViewData["IDPa"] = new SelectList(_context.PanierPrincs, "PID", "PID");
            ViewData["IDPro"] = new SelectList(_context.Produits, "IdPr", "Description");
            return View();
        }

        // POST: Paniers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IDPa,IDPro,Quantité,Total")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDPa"] = new SelectList(_context.PanierPrincs, "PID", "PID", panier.IDPa);
            ViewData["IDPro"] = new SelectList(_context.Produits, "IdPr", "Description", panier.IDPro);
            return View(panier);
        }

        // GET: Paniers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }
            ViewData["IDPa"] = new SelectList(_context.PanierPrincs, "PID", "PID", panier.IDPa);
            ViewData["IDPro"] = new SelectList(_context.Produits, "IdPr", "Description", panier.IDPro);
            return View(panier);
        }

        // POST: Paniers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDPa,IDPro,Quantité,Total")] Panier panier)
        {
            if (id != panier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.Id))
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
            ViewData["IDPa"] = new SelectList(_context.PanierPrincs, "PID", "PID", panier.IDPa);
            ViewData["IDPro"] = new SelectList(_context.Produits, "IdPr", "Description", panier.IDPro);
            return View(panier);
        }

        // GET: Paniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .Include(p => p.PanierPrinc)
                .Include(p => p.produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Panier == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Panier'  is null.");
            }
            var panier = await _context.Panier.FindAsync(id);
            if (panier != null)
            {
                _context.Panier.Remove(panier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
          return (_context.Panier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
