using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VueClient.Data;
using VueClient.Models;
using VueClient.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VueClient.Models.Enum;

namespace VueClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Users> _passwordHasher;

        public AuthController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Users>();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.users
                .FirstOrDefaultAsync(u =>
                    (u.email == model.Login || u.tel == model.Login) &&
                    (u.is_archived == null || u.is_archived == false)
                );

            if (user == null)
            {
                ModelState.AddModelError("", "Identifiants incorrects");
                return View(model);
            }

            bool passwordOk;
            try
            {
                var result = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.password,
                    model.Password
                );
                passwordOk = result == PasswordVerificationResult.Success;
            }
            catch (FormatException)
            {
                passwordOk = user.password == model.Password;
            }

            if (!passwordOk)
            {
                ModelState.AddModelError("", "Identifiants incorrects");
                return View(model);
            }

            // ✅ CLAIMS CORRECTS
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nom),
                new Claim(ClaimTypes.Email, user.email),
                new Claim("UserId", user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal
            );

            // ✅ SWITCH SUR ENUM
            return user.role switch
            {
                RoleUser.CLIENT => RedirectToAction("Index", "Home"),
                RoleUser.Gestionnaire => RedirectToAction("Dashboard", "Gestionnaire"),
                RoleUser.LIVREUR => RedirectToAction("Commandes", "Livreur"),
                _ => RedirectToAction("Login")
            };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
public async Task<IActionResult> Register(Users user)
{
    if (string.IsNullOrWhiteSpace(user.email) ||
        string.IsNullOrWhiteSpace(user.password) ||
        string.IsNullOrWhiteSpace(user.tel))
    {
        ModelState.AddModelError("", "Tous les champs sont obligatoires");
        return View(user);
    }

    bool exists = await _context.users.AnyAsync(u =>
        u.email == user.email || u.tel == user.tel
    );

    if (exists)
    {
        ModelState.AddModelError("", "Email ou téléphone déjà utilisé");
        return View(user);
    }

    user.role = RoleUser.CLIENT;
    user.is_archived = false;

    // ✅ ICI LA CORRECTION
    user.created_at = DateTime.Now;

    user.password = _passwordHasher.HashPassword(user, user.password);

    _context.users.Add(user);
    await _context.SaveChangesAsync();

    return RedirectToAction("Login");
}


    }
}
