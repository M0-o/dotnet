using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Library;

public class IndexModel : PageModel
{
    private readonly IBookService _bookService;

    public IndexModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IEnumerable<Book> BoughtBooks { get; set; } = Enumerable.Empty<Book>();
    public IEnumerable<Book> LentBooks { get; set; } = Enumerable.Empty<Book>();
    public string Filter { get; set; } = "all";

    public IActionResult OnGet(string? filter)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        Filter = filter?.ToLower() ?? "all";
        
        BoughtBooks = _bookService.GetUserBoughtBooks(userId.Value);
        LentBooks = _bookService.GetUserLentBooks(userId.Value);

        return Page();
    }
}
