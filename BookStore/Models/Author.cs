using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Author
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Bio { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
