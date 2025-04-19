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

        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Consumptions.Include(c => c.Veichle);
            return View(await appDBContext.ToListAsync());
        }

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

        public IActionResult Create()
        {
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Date,Value,Type,VeichleId")] Consumption consumption)
        {
            ModelState.Remove("Veichle");
            if (ModelState.IsValid)
            {
                _context.Add(consumption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VeichleId"] = new SelectList(_context.Veichles, "Id", "Brand", consumption.VeichleId);
            return View(consumption);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Date,Value,Type,VeichleId")] Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Veichle");
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
