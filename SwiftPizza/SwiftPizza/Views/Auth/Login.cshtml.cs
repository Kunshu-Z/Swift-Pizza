/// <summary>
/// Login.cshtml is the page which will contain all features concerning user login. It is linked with the user table in the database
/// to validate Email and Password inputs. If either of the fields are empty or contains invalid information, the code will push an error.
/// </summary>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SwiftPizza.Data;

namespace SwiftPizza.Views.Home
{
    public class LoginModel : PageModel
    {
        //Constructor that receives an instance of the application's database context.
        private readonly ApplicationDbContext _dbContext;

        //Bind properties for the following user input: Email and Password.
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        //Constructor to initialize the database context.
        public LoginModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Property to access the current HTTP context.
        public virtual HttpContext CurrentHttpContext => HttpContext;

        //Handler for HTTP GET requests.
        public void OnGet()
        {
            //Initialize an empty error message in the ViewData dictionary.
            ViewData["ErrorMessage"] = string.Empty;
        }

        //Handler for HTTP POST requests (form submission).
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                SetErrorMessage("Email and password are required.");
                return Page();
            }

            try
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    return RedirectToPage("/Index");
                }
                else
                {
                    SetErrorMessage("Invalid email or password.");
                    return Page();
                }
            }
            catch (Exception e)
            {
                SetErrorMessage("An error occurred while processing your request.");
                return Page();
            }
        }

        private void SetErrorMessage(string message)
        {
            if (ViewData != null)
            {
                ViewData["ErrorMessage"] = message;
            }
        }

    }
}
