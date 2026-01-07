using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure ApplicationUser
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired().HasMaxLength(256);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(256);
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(512);
        });

        // Configure Book
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(256);
            entity.Property(e => e.Author).IsRequired().HasMaxLength(256);
            entity.Property(e => e.Genre).IsRequired();
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.CoverImageUrl).HasMaxLength(512);
            entity.Property(e => e.Description).HasMaxLength(2000);
        });

        // Configure CartItem
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Book)
                .WithMany()
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Prevent duplicate cart items for same user and book
            entity.HasIndex(e => new { e.UserId, e.BookId }).IsUnique();
        });

        // Configure UserBook
        modelBuilder.Entity<UserBook>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Book)
                .WithMany()
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
