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
        /*private readonly IMedewerkerRepository _medewerkerRepository;
        public LoginController(IMedewerkerRepository medewerkerRepository)
        {
            _medewerkerRepository = medewerkerRepository;
        }*/
        private readonly IUsersService _usersService;
        public LoginController(IUsersService usersService)
        {
            _usersService = usersService;
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
        public ActionResult Login(LoginModel loginModel)
        {
            
            /*//get user (from repository/databas) matching username and password
            Medewerker? medewerker = _usersService.GetByLoginCredentials(loginModel.gebruikersnaam, loginModel.wachtwoord);
            //user will prob become bediening or employee
            ClaimsPrincipal medewerkers = HttpContext.User;
            if (medewerker == null)
            {
                ViewBag.ErrorMessage = "Incorrect username or password combination!";
                return View("Login");
            }
                HttpContext.Session.SetObject("LoggedInMedewerker", medewerker);
                return RedirectToAction("Index", "Table");*/
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, loginModel.bediening_ID.ToString()),
                new Claim(ClaimTypes.Name, loginModel.gebruikersnaam)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Table");
        }
        [HttpPost]
        public IActionResult LogOff(){
            //HttpContext.Session.Remove("LoggedInMedewerker");
            HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }
    }
}
