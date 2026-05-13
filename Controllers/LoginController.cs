using Chapeau.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Controllers
{
    public class LoginController : Controller
    {
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
            return RedirectToAction("Home", "Home");
        }
    }
}
