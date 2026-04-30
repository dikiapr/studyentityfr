using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
