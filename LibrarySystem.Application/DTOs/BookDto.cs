using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsBought { get; set; }
    public bool IsLent { get; set; }
    public string? CoverImageUrl { get; set; }
}
