using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace SwiftPizza.Views.Shared
{
    public abstract class RazorPageBase : PageModel
    {
        public string CurrentUserName => HttpContext.Session.GetString("FirstName");

        public bool IsUserLoggedIn => !string.IsNullOrEmpty(CurrentUserName);
    }
}
