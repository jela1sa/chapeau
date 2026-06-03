using Chapeau.Models;
using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Chapeau.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMedewerkersService _medewerkersService;
        public LoginController(IMedewerkersService medewerkersService)
        {
            _medewerkersService = medewerkersService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginModel loginModel)
        {
            
            Medewerker? medewerker = _medewerkersService.GetByLoginCredentials(loginModel.gebruikersnaam, loginModel.wachtwoord);
            if (medewerker == null)
            {
                ViewBag.ErrorMessage = "Incorrect username or password combination!";
                return View("Login");
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, medewerker.bediening_ID.ToString()),
                new Claim(ClaimTypes.Name, medewerker.gebruikersnaam),
                new Claim(ClaimTypes.Role, medewerker.rol),
                new Claim("FullName", medewerker.naam)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Tafel");
        }
        [HttpPost]
        public async Task<IActionResult> LogOffAsync()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }
    }
}
