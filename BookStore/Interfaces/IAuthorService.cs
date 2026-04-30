using BookStore.DTOs.Requests;
using BookStore.DTOs.Responses;

namespace BookStore.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponse>> GetAllAsync();
    Task<AuthorResponse?> GetByIdAsync(int id);
    Task<AuthorResponse> CreateAsync(CreateAuthorRequest request);
    Task<AuthorResponse?> UpdateAsync(int id, UpdateAuthorRequest request);
    Task<bool> DeleteAsync(int id);
}
