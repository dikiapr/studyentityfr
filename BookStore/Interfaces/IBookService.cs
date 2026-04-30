using BookStore.DTOs.Requests;
using BookStore.DTOs.Responses;

namespace BookStore.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync();
    Task<BookResponse?> GetByIdAsync(int id);
    Task<BookResponse?> CreateAsync(CreateBookRequest request);
    Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteAsync(int id);
}
