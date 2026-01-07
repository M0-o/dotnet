using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");
    }

    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");
    }
}
