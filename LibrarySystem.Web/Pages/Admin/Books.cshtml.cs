using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrarySystem.Web.Pages.Admin;

public class BooksModel : PageModel
{
    private readonly IBookService _bookService;

    public BooksModel(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();

    [BindProperty]
    public Book BookInput { get; set; } = new();

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public IActionResult OnGet()
    {
        // Check if user is admin
        var isAdmin = HttpContext.Session.GetString("IsAdmin");
        if (isAdmin != "True")
        {
            return RedirectToPage("/Dashboard/Index");
        }

        Books = _bookService.GetAllBooks();
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        var isAdmin = HttpContext.Session.GetString("IsAdmin");
        if (isAdmin != "True")
        {
            return RedirectToPage("/Dashboard/Index");
        }

        try
        {
            _bookService.DeleteBook(id);
            TempData["SuccessMessage"] = "Book deleted successfully!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Error deleting book. It may be in use.";
        }
        
        return RedirectToPage();
    }

    public IActionResult OnPostAdd()
    {
        var isAdmin = HttpContext.Session.GetString("IsAdmin");
        if (isAdmin != "True")
        {
            return RedirectToPage("/Dashboard/Index");
        }

        try
        {
            _bookService.AddBook(BookInput);
            TempData["SuccessMessage"] = "Book added successfully!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Error adding book.";
        }

        return RedirectToPage();
    }

    public IActionResult OnPostUpdate()
    {
        var isAdmin = HttpContext.Session.GetString("IsAdmin");
        if (isAdmin != "True")
        {
            return RedirectToPage("/Dashboard/Index");
        }

        try
        {
            _bookService.UpdateBook(BookInput);
            TempData["SuccessMessage"] = "Book updated successfully!";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Error updating book.";
        }

        return RedirectToPage();
    }
}
