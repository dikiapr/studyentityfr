using BookStore.Data;
using BookStore.DTOs.Requests;
using BookStore.DTOs.Responses;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorResponse>> GetAllAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .Select(a => ToResponse(a))
            .ToListAsync();
    }

    public async Task<AuthorResponse?> GetByIdAsync(int id)
    {
        var author = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        return author is null ? null : ToResponse(author);
    }

    public async Task<AuthorResponse> CreateAsync(CreateAuthorRequest request)
    {
        var author = new Author
        {
            Name = request.Name,
            Bio = request.Bio,
            CreatedAt = DateTime.UtcNow
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return ToResponse(author);
    }

    public async Task<AuthorResponse?> UpdateAsync(int id, UpdateAuthorRequest request)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author is null) return null;

        author.Name = request.Name;
        author.Bio = request.Bio;

        await _context.SaveChangesAsync();
        return ToResponse(author);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author is null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    private static AuthorResponse ToResponse(Author a) =>
        new(a.Id, a.Name, a.Bio, a.CreatedAt);
}
