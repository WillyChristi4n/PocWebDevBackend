using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocWebDevBackend.Models;
using PocWebDevBackend.Service.Auth;
using PocWebDevBackend.Service.Toast;

namespace PocWebDevBackend.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IEncriptService _encriptService;

        public UsersController(AppDBContext context, IEncriptService encriptService)
        {
            _context = context;
            _encriptService = encriptService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Role")] User user)
        {

            if (ModelState.IsValid)
            {
                var cript = _encriptService.HashPassword(user, user.Password);
                user.Password = cript;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cript = _encriptService.HashPassword(user, user.Password);
                    user.Password = cript;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Valide user exist
            var user = await _context.User.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                TempData["Toast"] = JsonSerializer.Serialize(new ToastService
                {
                    Message = "Usuário ou senha inválido",
                    Type = ToastService.MessageType.Error
                });
                return View();
            }

            // Validate Password
            bool isValidPassword = _encriptService.VerifyPassword(user, user.Password, password);
            if (!isValidPassword)
            {
                TempData["Toast"] = JsonSerializer.Serialize(new ToastService
                {
                    Message = "Usuário ou senha inválido",
                    Type = ToastService.MessageType.Error
                });
                return View();
            }

            var userClaimList = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
            };
            var userIdentity = new ClaimsIdentity(userClaimList, "login");
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
            var authProps = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(3),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProps
            );

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Users");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
