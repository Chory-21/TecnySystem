using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TecnySystem.Models;

namespace TecnySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                HttpContext.Session.SetString("User", username);
                return RedirectToAction("Inicio", "Inicio"); // Redirige a la vista Inicio en InicioController
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View("Index"); // Vuelve a la vista de login
            }
        }



        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("User") == "admin")
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}