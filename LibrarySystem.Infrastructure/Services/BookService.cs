using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public IEnumerable<Book> GetBooksByGenres(IEnumerable<Genre> genres)
    {
        var genreList = genres.ToList();
        if (!genreList.Any())
            return GetAllBooks();
            
        return _context.Books.Where(b => genreList.Contains(b.Genre)).ToList();
    }

    public Book? GetBookById(int id)
    {
        return _context.Books.Find(id);
    }

    public IEnumerable<Book> GetUserBoughtBooks(int userId)
    {
        return _context.UserBooks
            .Where(ub => ub.UserId == userId && ub.IsBought)
            .Include(ub => ub.Book)
            .Select(ub => ub.Book)
            .ToList();
    }

    public IEnumerable<Book> GetUserLentBooks(int userId)
    {
        var lentBooks = _context.UserBooks
            .Where(ub => ub.UserId == userId && ub.IsLent)
            .Include(ub => ub.Book)
            .ToList();

        return lentBooks.Select(ub =>
        {
            var book = ub.Book;
            // Create a copy with due date information
            return new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Price = book.Price,
                IsAvailable = book.IsAvailable,
                CoverImageUrl = book.CoverImageUrl,
                Description = book.Description,
                IsLent = true,
                DueDate = ub.DueDate
            };
        }).ToList();
    }

    public void MarkAsBought(int bookId, int userId)
    {
        var existingRecord = _context.UserBooks
            .FirstOrDefault(ub => ub.UserId == userId && ub.BookId == bookId && ub.IsBought);

        if (existingRecord == null)
        {
            _context.UserBooks.Add(new UserBook
            {
                UserId = userId,
                BookId = bookId,
                IsBought = true,
                IsLent = false,
                AcquiredAt = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }

    public void MarkAsLent(int bookId, int userId, DateTime dueDate)
    {
        var existingRecord = _context.UserBooks
            .FirstOrDefault(ub => ub.UserId == userId && ub.BookId == bookId && ub.IsLent);

        if (existingRecord == null)
        {
            _context.UserBooks.Add(new UserBook
            {
                UserId = userId,
                BookId = bookId,
                IsBought = false,
                IsLent = true,
                DueDate = dueDate,
                AcquiredAt = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public void DeleteBook(int bookId)
    {
        var book = _context.Books.Find(bookId);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
