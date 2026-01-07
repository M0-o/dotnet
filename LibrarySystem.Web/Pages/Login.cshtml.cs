using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public LoginDto Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public IActionResult OnGet()
    {
        // If already logged in, redirect to dashboard
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToPage("/Dashboard/Index");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _userService.Authenticate(Input.Email, Input.Password);
        
        if (user == null)
        {
            ErrorMessage = "Invalid email or password. Please try again.";
            return Page();
        }

        // Store user info in session
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserProfilePic", user.ProfilePictureUrl ?? "");
        HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

        return RedirectToPage("/Dashboard/Index");
    }
}
