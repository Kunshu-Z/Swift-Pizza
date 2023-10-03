using Microsoft.AspNetCore.Mvc;
using SwiftPizza.Data;
using SwiftPizza.Models;
using System.Diagnostics;

namespace SwiftPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Pizzas()
        {
            var pizzas = _dbContext.Pizza.ToList();
            return View(pizzas);
        }

        public IActionResult Login()
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