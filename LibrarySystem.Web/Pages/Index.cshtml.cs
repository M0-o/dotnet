using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        // Redirect to Dashboard if logged in, otherwise to Login
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToPage("/Dashboard/Index");
        }
        return RedirectToPage("/Login");
    }
}
