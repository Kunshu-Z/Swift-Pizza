using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using SwiftPizza.Data;
using SwiftPizza.Models;

namespace SwiftPizza.Views.Home
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        public string Input_Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Input_Password { get; set; }

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
            if (ModelState.IsValid)
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Input_Email);

                if (user != null && user.Password == Input_Password)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.FirstName), // Assuming you have a "FirstName" property in your User model
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
                }
            }

            // If we reach here, there was a validation error, so return to the login page.
            return Page();
        }
    }
}
