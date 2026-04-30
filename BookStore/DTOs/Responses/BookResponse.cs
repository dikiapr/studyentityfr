namespace BookStore.DTOs.Responses;

public record BookResponse(
    int Id,
    string Title,
    decimal Price,
    int Stock,
    DateTime CreatedAt,
    AuthorResponse Author
);
