using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Dashboard;

public class IndexModel : PageModel
{
    private readonly IBookService _bookService;
    private readonly ICartService _cartService;

    public IndexModel(IBookService bookService, ICartService cartService)
    {
        _bookService = bookService;
        _cartService = cartService;
    }

    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    public List<Genre> AllGenres { get; set; } = Enum.GetValues<Genre>().ToList();
    public List<Genre> SelectedGenres { get; set; } = new();

    public IActionResult OnGet([FromQuery] List<Genre> selectedGenres)
    {
        // Check authentication
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToPage("/Login");
        }

        SelectedGenres = selectedGenres ?? new List<Genre>();
        
        if (SelectedGenres.Any())
        {
            Books = _bookService.GetBooksByGenres(SelectedGenres);
        }
        else
        {
            Books = _bookService.GetAllBooks();
        }

        return Page();
    }

    public IActionResult OnPostAddToCart(int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        _cartService.AddToCart(userId.Value, bookId);
        
        TempData["Message"] = "Book added to cart!";
        return RedirectToPage();
    }
}
