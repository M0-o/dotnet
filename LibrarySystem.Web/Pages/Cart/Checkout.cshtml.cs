using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Cart;

public class CheckoutModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IBookService _bookService;

    public CheckoutModel(ICartService cartService, IBookService bookService)
    {
        _cartService = cartService;
        _bookService = bookService;
    }

    public IEnumerable<Book> CartItems { get; set; } = Enumerable.Empty<Book>();
    public decimal TotalPrice => CartItems.Sum(b => b.Price);
    
    [TempData]
    public string? SuccessMessage { get; set; }
    
    [TempData]
    public string? ErrorMessage { get; set; }

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

    public IActionResult OnPostBuy(int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var book = _bookService.GetBookById(bookId);
            if (book == null)
            {
                ErrorMessage = "Book not found.";
                return RedirectToPage();
            }

            // Mark the book as bought
            _bookService.MarkAsBought(bookId, userId.Value);
            
            // Remove from cart
            _cartService.RemoveFromCart(userId.Value, bookId);
            
            SuccessMessage = $"Successfully purchased '{book.Title}'! You can now access it from your dashboard.";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error processing purchase: {ex.Message}";
            return RedirectToPage();
        }
    }

    public IActionResult OnPostBorrow(int bookId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var book = _bookService.GetBookById(bookId);
            if (book == null)
            {
                ErrorMessage = "Book not found.";
                return RedirectToPage();
            }

            // Set due date to 14 days from now
            var dueDate = DateTime.UtcNow.AddDays(14);
            
            // Mark the book as borrowed
            _bookService.MarkAsLent(bookId, userId.Value, dueDate);
            
            // Remove from cart
            _cartService.RemoveFromCart(userId.Value, bookId);
            
            SuccessMessage = $"Successfully borrowed '{book.Title}'! Due date: {dueDate:MMMM dd, yyyy}";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error processing borrow: {ex.Message}";
            return RedirectToPage();
        }
    }
}
