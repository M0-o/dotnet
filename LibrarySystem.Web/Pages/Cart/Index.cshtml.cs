using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;

    public IndexModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IEnumerable<Book> CartItems { get; set; } = Enumerable.Empty<Book>();
    public decimal TotalPrice => CartItems.Sum(b => b.Price);

    public IActionResult OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        CartItems = _cartService.GetCartItems(userId.Value);
        return Page();
    }

    public IActionResult OnPostRemove(int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        _cartService.RemoveFromCart(userId.Value, bookId);
        return RedirectToPage();
    }

    public IActionResult OnPostClear()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        _cartService.ClearCart(userId.Value);
        return RedirectToPage();
    }
}
