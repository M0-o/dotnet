using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Profile;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IBookService _bookService;

    public IndexModel(IUserService userService, IBookService bookService)
    {
        _userService = userService;
        _bookService = bookService;
    }

    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public IEnumerable<Book> LentBooks { get; set; } = Enumerable.Empty<Book>();
    public IEnumerable<Book> BoughtBooks { get; set; } = Enumerable.Empty<Book>();

    public IActionResult OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        LoadUserData(userId.Value);
        return Page();
    }

    public IActionResult OnPostUpdateName(string newName)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        if (!string.IsNullOrWhiteSpace(newName))
        {
            _userService.UpdateUserProfile(userId.Value, newName, null);
            HttpContext.Session.SetString("UserName", newName);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostUpdatePicture(string pictureUrl)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        var user = _userService.GetUserById(userId.Value);
        if (user != null && !string.IsNullOrWhiteSpace(pictureUrl))
        {
            _userService.UpdateUserProfile(userId.Value, user.FullName, pictureUrl);
            HttpContext.Session.SetString("UserProfilePic", pictureUrl);
        }

        return RedirectToPage();
    }

    private void LoadUserData(int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user != null)
        {
            UserName = user.FullName;
            UserEmail = user.Email;
            ProfilePictureUrl = user.ProfilePictureUrl ?? "https://ui-avatars.com/api/?name=User&background=6366f1&color=fff&size=128";
        }
        
        LentBooks = _bookService.GetUserLentBooks(userId);
        BoughtBooks = _bookService.GetUserBoughtBooks(userId);
    }
}
