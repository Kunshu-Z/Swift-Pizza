using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwiftPizza.Data;
using SwiftPizza.Models;
using System.Linq;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public LoginModel(ILogger<LoginModel> logger, ApplicationDbContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        // Retrieve user from the database based on the provided email
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);

        if (user != null && user.Password == Password)
        {
            // Authentication successful, you can store user information in a session or cookie
            // For simplicity, this example just redirects to a protected page
            return RedirectToPage("/ProtectedPage");
        }

        // Authentication failed
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

}
