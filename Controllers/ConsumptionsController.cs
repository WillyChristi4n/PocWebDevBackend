using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PocWebDevBackend.Models;

namespace PocWebDevBackend.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly AppDBContext _context;

        public ConsumptionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Consumptions
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Consumptions.Include(c => c.Veichle);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Consumptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumptions
                .Include(c => c.Veichle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumption == null)
            {
                return NotFound();
            }

            return View(consumption);
        }

        // GET: Consumptions/Create
        public IActionResult Create()
        {
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand");
            return View();
        }

        // POST: Consumptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Date,Value,Type,VeichleId")] Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand", consumption.VeichleId);
            return View(consumption);
        }

        // GET: Consumptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumptions.FindAsync(id);
            if (consumption == null)
            {
                return NotFound();
            }
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand", consumption.VeichleId);
            return View(consumption);
        }

        // POST: Consumptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Date,Value,Type,VeichleId")] Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumptionExists(consumption.Id))
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
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand", consumption.VeichleId);
            return View(consumption);
        }

        // GET: Consumptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumption = await _context.Consumptions
                .Include(c => c.Veichle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumption == null)
            {
                return NotFound();
            }

            return View(consumption);
        }

        // POST: Consumptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumption = await _context.Consumptions.FindAsync(id);
            if (consumption != null)
            {
                _context.Consumptions.Remove(consumption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumptionExists(int id)
        {
            return _context.Consumptions.Any(e => e.Id == id);
        }
    }
}
