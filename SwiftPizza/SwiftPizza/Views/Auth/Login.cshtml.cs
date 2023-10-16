using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public virtual HttpContext CurrentHttpContext => HttpContext;

        public void OnGet()
        {
            ViewData["ErrorMessage"] = string.Empty;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewData["ErrorMessage"] = "Email and password are required.";
                return Page();
            }

            try
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                // Check if user is found and credentials are correct
                if (user != null)
                {
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    return RedirectToPage("/Index");
                }
                // If user is not found or credentials are incorrect, set error message
                else
                {
                    ViewData["ErrorMessage"] = "Invalid email or password.";
                    return Page();
                }
            }
            catch (Exception e)
            {
                // Log the exception (ideally using a logging framework)
                ViewData["ErrorMessage"] = "An error occurred while processing your request.";
                return Page();
            }
        }

    }
}
