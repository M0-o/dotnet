using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetCartItems(int userId)
    {
        return _context.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Book)
            .Select(c => c.Book)
            .ToList();
    }

    public void AddToCart(int userId, int bookId)
    {
        var existingItem = _context.CartItems
            .FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);

        if (existingItem == null)
        {
            _context.CartItems.Add(new CartItem
            {
                UserId = userId,
                BookId = bookId,
                AddedAt = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }

    public void RemoveFromCart(int userId, int bookId)
    {
        var item = _context.CartItems
            .FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);
        
        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
    }

    public void ClearCart(int userId)
    {
        var items = _context.CartItems.Where(c => c.UserId == userId);
        _context.CartItems.RemoveRange(items);
        _context.SaveChanges();
    }

    public int GetCartCount(int userId)
    {
        return _context.CartItems.Count(c => c.UserId == userId);
    }
}
