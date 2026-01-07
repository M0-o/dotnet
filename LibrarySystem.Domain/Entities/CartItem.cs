namespace LibrarySystem.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
