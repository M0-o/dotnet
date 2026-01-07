using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime? DueDate { get; set; }
    public bool IsBought { get; set; }
    public bool IsLent { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Description { get; set; }
}
