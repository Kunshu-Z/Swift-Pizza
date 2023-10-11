using Microsoft.AspNetCore.Mvc;
using SwiftPizza.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SwiftPizza.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Store the user's first name in the session
                HttpContext.Session.SetString("FirstName", user.FirstName);

                return RedirectToAction("Index", "Home"); // Redirect to Home/Index after successful login
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid email or password.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            // Remove user data from session
            HttpContext.Session.Remove("FirstName");
            return RedirectToAction("Login");
        }
    }
}

