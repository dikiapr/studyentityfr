namespace BookStore.DTOs.Responses;

public record AuthorResponse(
    int Id,
    string Name,
    string? Bio,
    DateTime CreatedAt
);
