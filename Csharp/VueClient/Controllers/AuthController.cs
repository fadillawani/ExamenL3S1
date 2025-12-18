using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VueClient.Data;
using VueClient.Models;
using VueClient.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            // Chercher l'utilisateur par email ou téléphone
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

            // Vérifier le mot de passe
            bool passwordOk = false;
            try
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.password, model.Password);
                passwordOk = result == PasswordVerificationResult.Success;
            }
            catch (FormatException)
            {
                // Cas mot de passe en clair (si nécessaire pour démo/examen)
                passwordOk = user.password == model.Password;
            }

            if (!passwordOk)
            {
                ModelState.AddModelError("", "Identifiants incorrects");
                return View(model);
            }

            // Créer les claims pour l'authentification
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nom),
                new Claim(ClaimTypes.Email, user.email),
                new Claim("UserId", user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Se connecter avec cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // Redirection selon le rôle
            return user.role switch
            {
                "CLIENT" => RedirectToAction("Index", "Home"),
                "GESTIONNAIRE" => RedirectToAction("Dashboard", "Gestionnaire"),
                "LIVREUR" => RedirectToAction("Commandes", "Livreur"),
                _ => RedirectToAction("Login")
            };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
