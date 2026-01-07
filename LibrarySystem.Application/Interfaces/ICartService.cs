using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces;

public interface ICartService
{
    IEnumerable<Book> GetCartItems(int userId);
    void AddToCart(int userId, int bookId);
    void RemoveFromCart(int userId, int bookId);
    void ClearCart(int userId);
    int GetCartCount(int userId);
}
