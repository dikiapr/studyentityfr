using BookStore.Data;
using BookStore.DTOs;
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

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .Select(b => ToDto(b))
            .ToListAsync();
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return book is null ? null : ToDto(book);
    }

    public async Task<BookResponseDto> CreateAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return ToDto(book);
    }

    public async Task<BookResponseDto?> UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return null;

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Price = dto.Price;
        book.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return ToDto(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BookResponseDto ToDto(Book b) =>
        new(b.Id, b.Title, b.Author, b.Price, b.Stock, b.CreatedAt);
}
