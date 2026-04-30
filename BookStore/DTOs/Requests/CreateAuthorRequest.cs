using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.Requests;

public class CreateAuthorRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Bio { get; set; }
}
