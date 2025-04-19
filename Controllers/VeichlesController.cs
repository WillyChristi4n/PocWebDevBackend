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

        // GET: Veichles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veichles.ToListAsync());
        }

        // GET: Veichles/Details/5
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

        // GET: Veichles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veichles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Veichles/Edit/5
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

        // POST: Veichles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Veichles/Delete/5
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

        // POST: Veichles/Delete/5
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
