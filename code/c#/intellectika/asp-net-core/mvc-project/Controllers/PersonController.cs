using Microsoft.AspNetCore.Mvc;
using mvcproject.Models;
using System.Diagnostics;

namespace mvcproject.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public PersonController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
