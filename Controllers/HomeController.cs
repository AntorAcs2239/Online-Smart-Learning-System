using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online__Smart_Learning_System.Models;
using System.Diagnostics;

namespace Online__Smart_Learning_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        List<string> data = new List<string>();
        public IActionResult Index()
        {

            return View("HelloWorld");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
