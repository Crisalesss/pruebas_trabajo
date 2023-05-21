using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTreeView.Models;
using WebTreeView.Utilidades;

namespace WebTreeView.Controllers
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
            string jsonFilePath = @"C:\Temporal\Items.json";
            List<Item> ResultJson = LeerJson.Leer(jsonFilePath);

            return View(ResultJson);
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