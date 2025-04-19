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
    public class VeichlesController : Controller
    {
        private readonly AppDBContext _context;

        public VeichlesController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Veichles.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veichle = await _context.Veichles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veichle == null)
            {
                return NotFound();
            }

            return View(veichle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,YearOfManufactor,YearOfModel,Plate")] Veichle veichle)
        {
            ModelState.Remove("Consumptions");
            if (ModelState.IsValid)
            {
                _context.Add(veichle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veichle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veichle = await _context.Veichles.FindAsync(id);
            if (veichle == null)
            {
                return NotFound();
            }
            return View(veichle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,YearOfManufactor,YearOfModel,Plate")] Veichle veichle)
        {
            if (id != veichle.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Consumptions");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veichle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeichleExists(veichle.Id))
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
            return View(veichle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veichle = await _context.Veichles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veichle == null)
            {
                return NotFound();
            }

            return View(veichle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veichle = await _context.Veichles.FindAsync(id);
            if (veichle != null)
            {
                _context.Veichles.Remove(veichle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeichleExists(int id)
        {
            return _context.Veichles.Any(e => e.Id == id);
        }
    }
}
