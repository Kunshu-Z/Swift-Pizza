using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SwiftPizza.Models; // Import your User model
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

        public LoginModel(ApplicationDbContext dbContext) // Inject your ApplicationDbContext
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // This is the initial GET request to display the login form.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Query the database to find a user with the provided email.
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Email);

            if (user != null && VerifyPassword(user.Password, Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email), // Store the user's email as a claim.
                    // You can add more claims here if needed.
                };

                var identity = new ClaimsIdentity(claims, "cookie");

                var principal = new ClaimsPrincipal(identity);

                // Sign in the user with the authentication cookie.
                await HttpContext.SignInAsync(principal);

                // Redirect to a protected resource or dashboard.
                return RedirectToPage("/Dashboard"); // Replace with your desired redirect page.
            }
            else
            {
                // Invalid credentials - show an error message.
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }
        }

        // You should implement a proper password verification method.
        private bool VerifyPassword(string storedPasswordHash, string inputPassword)
        {
            // Implement your password verification logic here.
            // This might involve using a secure hashing algorithm.
            // Return true if the input password matches the stored hash.
            // Otherwise, return false.
            // Replace this with your actual password verification logic.
            return storedPasswordHash == inputPassword;
        }
    }
}
