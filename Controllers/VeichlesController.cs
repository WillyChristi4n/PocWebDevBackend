
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocWebDevBackend.Models;

namespace PocWebDevBackend.Controllers
{
    public class VeichlesController : Controller
    {
        private AppDBContext context;

        public VeichlesController(AppDBContext appContext)
        {
            context = appContext;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await context.Veichles.ToListAsync();

            return View(dados);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Veichle veichle)
        {
            if (ModelState.IsValid)
            {
                context.Veichles.Add(veichle);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(veichle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null) return NotFound();

            var veichle = await context.Veichles.FindAsync(id);

            if (veichle == null) return NotFound();

            return View(veichle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Veichle veichle)
        {
            if (id == null || id != veichle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                context.Veichles.Update(veichle);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(veichle);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null) return NotFound();

            var veichle = await context.Veichles.FindAsync(id);

            if (veichle == null) return NotFound();

            return View(veichle);
        }

        public async Task<IActionResult> Delete(int? id, Veichle veichle)
        {
            if (id == null || id != veichle.Id) return NotFound();
            context.Veichles.Remove(veichle);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
