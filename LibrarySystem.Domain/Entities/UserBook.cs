namespace LibrarySystem.Domain.Entities;

public class UserBook
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public bool IsBought { get; set; }
    public bool IsLent { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
