using BookStore.Data;
using BookStore.DTOs.Requests;
using BookStore.DTOs.Responses;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookResponse>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .Select(b => ToResponse(b))
            .ToListAsync();
    }

    public async Task<BookResponse?> GetByIdAsync(int id)
    {
        var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return book is null ? null : ToResponse(book);
    }

    public async Task<BookResponse> CreateAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return ToResponse(book);
    }

    public async Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return null;

        book.Title = request.Title;
        book.Author = request.Author;
        book.Price = request.Price;
        book.Stock = request.Stock;

        await _context.SaveChangesAsync();
        return ToResponse(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BookResponse ToResponse(Book b) =>
        new(b.Id, b.Title, b.Author, b.Price, b.Stock, b.CreatedAt);
}
