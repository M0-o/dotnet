using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Application.Interfaces;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks();
    IEnumerable<Book> GetBooksByGenres(IEnumerable<Genre> genres);
    IEnumerable<Book> GetUserBoughtBooks(int userId);
    IEnumerable<Book> GetUserLentBooks(int userId);
    Book? GetBookById(int id);
    void MarkAsBought(int bookId, int userId);
    void MarkAsLent(int bookId, int userId, DateTime dueDate);
    
    // Admin CRUD operations
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int bookId);
}
