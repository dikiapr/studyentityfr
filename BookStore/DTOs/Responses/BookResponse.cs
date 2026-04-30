namespace BookStore.DTOs.Responses;

public record BookResponse(
    int Id,
    string Title,
    string Author,
    decimal Price,
    int Stock,
    DateTime CreatedAt
);
