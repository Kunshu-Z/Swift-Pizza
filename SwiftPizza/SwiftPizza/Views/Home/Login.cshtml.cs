using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SwiftPizza.Models;
using SwiftPizza.Data;

namespace SwiftPizza.Views.Home
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // This is the initial GET request to display the login form.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Email);

            if (user != null && user.Password == Password)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Email),
            // You can add more claims here if needed.
        };

                var identity = new ClaimsIdentity(claims, "cookie");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                // Redirect to the index page after a successful login.
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }
        }

    }
}
