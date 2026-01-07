using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Users.Any())
        {
            return; // Database has been seeded
        }

        // Seed Users
        var users = new[]
        {
            new ApplicationUser
            {
                Email = "admin@library.com",
                Password = "admin123",
                FullName = "System Administrator",
                IsAdmin = true,
                ProfilePictureUrl = "https://ui-avatars.com/api/?name=Admin&background=dc3545&color=fff&size=128",
                CreatedAt = DateTime.UtcNow
            },
            new ApplicationUser
            {
                Email = "member@gmail.com",
                Password = "member123",
                FullName = "John Member",
                IsAdmin = false,
                ProfilePictureUrl = "https://ui-avatars.com/api/?name=John+Member&background=6366f1&color=fff&size=128",
                CreatedAt = DateTime.UtcNow.AddMonths(-6)
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        // Seed Books
        var books = new[]
        {
            new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = Genre.Fiction, Price = 12.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book1/200/300", Description = "A classic American novel" },
            new Book { Title = "1984", Author = "George Orwell", Genre = Genre.ScienceFiction, Price = 14.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book2/200/300", Description = "Dystopian social science fiction" },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = Genre.Fiction, Price = 11.99m, IsAvailable = false, CoverImageUrl = "https://picsum.photos/seed/book3/200/300", Description = "A gripping story of justice" },
            new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = Genre.Fantasy, Price = 15.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book4/200/300", Description = "An unexpected journey" },
            new Book { Title = "Murder on the Orient Express", Author = "Agatha Christie", Genre = Genre.Mystery, Price = 10.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book5/200/300", Description = "A classic whodunit" },
            new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = Genre.Romance, Price = 9.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book6/200/300", Description = "A timeless love story" },
            new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = Genre.Thriller, Price = 13.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book7/200/300", Description = "A pulse-pounding thriller" },
            new Book { Title = "Steve Jobs", Author = "Walter Isaacson", Genre = Genre.Biography, Price = 18.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book8/200/300", Description = "Biography of the Apple founder" },
            new Book { Title = "Sapiens", Author = "Yuval Noah Harari", Genre = Genre.History, Price = 16.99m, IsAvailable = false, CoverImageUrl = "https://picsum.photos/seed/book9/200/300", Description = "A brief history of humankind" },
            new Book { Title = "Atomic Habits", Author = "James Clear", Genre = Genre.SelfHelp, Price = 14.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book10/200/300", Description = "Build good habits, break bad ones" },
            new Book { Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Genre = Genre.Fantasy, Price = 12.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book11/200/300", Description = "The boy who lived" },
            new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = Genre.Technology, Price = 39.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book12/200/300", Description = "A handbook of agile software craftsmanship" },
            new Book { Title = "The Shining", Author = "Stephen King", Genre = Genre.Thriller, Price = 11.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book13/200/300", Description = "A spine-chilling horror classic" },
            new Book { Title = "Dune", Author = "Frank Herbert", Genre = Genre.ScienceFiction, Price = 17.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book14/200/300", Description = "Epic science fiction adventure" },
            new Book { Title = "The Alchemist", Author = "Paulo Coelho", Genre = Genre.Fiction, Price = 10.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book15/200/300", Description = "A fable about following your dreams" },
            new Book { Title = "Gone Girl", Author = "Gillian Flynn", Genre = Genre.Mystery, Price = 12.99m, IsAvailable = true, CoverImageUrl = "https://picsum.photos/seed/book16/200/300", Description = "A twisted psychological thriller" }
        };
        context.Books.AddRange(books);
        context.SaveChanges();

        // Seed some user book relationships for the demo user (ID will be 2)
        var userId = users[1].Id;
        var userBooks = new[]
        {
            // Bought books
            new UserBook { UserId = userId, BookId = 1, IsBought = true, IsLent = false, AcquiredAt = DateTime.UtcNow.AddMonths(-3) },
            new UserBook { UserId = userId, BookId = 5, IsBought = true, IsLent = false, AcquiredAt = DateTime.UtcNow.AddMonths(-2) },
            new UserBook { UserId = userId, BookId = 12, IsBought = true, IsLent = false, AcquiredAt = DateTime.UtcNow.AddMonths(-1) },
            
            // Lent books
            new UserBook { UserId = userId, BookId = 3, IsBought = false, IsLent = true, DueDate = DateTime.UtcNow.AddDays(7), AcquiredAt = DateTime.UtcNow.AddDays(-7) },
            new UserBook { UserId = userId, BookId = 9, IsBought = false, IsLent = true, DueDate = DateTime.UtcNow.AddDays(14), AcquiredAt = DateTime.UtcNow.AddDays(-7) },
            new UserBook { UserId = userId, BookId = 4, IsBought = false, IsLent = true, DueDate = DateTime.UtcNow.AddDays(-2), AcquiredAt = DateTime.UtcNow.AddDays(-23) } // Overdue
        };
        context.UserBooks.AddRange(userBooks);
        context.SaveChanges();
    }
}
