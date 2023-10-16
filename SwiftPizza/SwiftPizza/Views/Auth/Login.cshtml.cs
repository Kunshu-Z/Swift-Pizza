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
            //Checking if the Email or Password fields are empty.
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                //Setting an error message in ViewData and return the page.
                ViewData["ErrorMessage"] = "Email and password are required.";
                return Page();
            }

            try
            {
                //Attempting to find a user with the provided Email and Password in the database.
                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                //Checking if a user is found and if the provided credentials are correct.
                if (user != null)
                {
                    //Setting the user's first name in the session and redirect them to the "Index" page.
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    return RedirectToPage("/Index");
                }
                //If the user is not found or the credentials are incorrect, push an error message.
                else
                {
                    ViewData["ErrorMessage"] = "Invalid email or password.";
                    return Page();
                }
            }
            catch (Exception e)
            {
                //Logging the exception (ideally using a logging framework), setting an error message, and return the page.
                ViewData["ErrorMessage"] = "An error occurred while processing your request.";
                return Page();
            }
        }
    }
}
