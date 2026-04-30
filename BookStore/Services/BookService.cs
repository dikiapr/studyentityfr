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
            .Include(b => b.Author)
            .Select(b => ToResponse(b))
            .ToListAsync();
    }

    public async Task<BookResponse?> GetByIdAsync(int id)
    {
        Book? book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        return book is null ? null : ToResponse(book);
    }

    public async Task<BookResponse?> CreateAsync(CreateBookRequest request)
    {
        bool authorExists = await _context.Authors.AnyAsync(a => a.Id == request.AuthorId);
        if (!authorExists)
        {
            return null;   
        }
        Book book = new Book()
        {
            Title = request.Title,
            AuthorId = request.AuthorId,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        await _context.Entry(book).Reference(b => b.Author).LoadAsync();
        return ToResponse(book);
    }

    public async Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request)
    {
        Book? book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
        {
            return null;            
        }
        bool authorExists = await _context.Authors.AnyAsync(a => a.Id == request.AuthorId);
        if (!authorExists)
        {
            return null;   
        }
        book.Title = request.Title;
        book.AuthorId = request.AuthorId;
        book.Price = request.Price;
        book.Stock = request.Stock;

        await _context.SaveChangesAsync();

        if (book.Author is null || book.Author.Id != book.AuthorId)
        {
            await _context.Entry(book).Reference(b => b.Author).LoadAsync();
        }

        return ToResponse(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null)
        {
            return false;            
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BookResponse ToResponse(Book book)
    {
        return new BookResponse(
            Id: book.Id,
            Title: book.Title,
            Price: book.Price,
            Stock: book.Stock,
            CreatedAt: book.CreatedAt,
            Author: new AuthorResponse(
                Id: book.Author!.Id,
                Name: book.Author.Name,
                Bio: book.Author.Bio,
                CreatedAt: book.Author.CreatedAt
            )
        );
    }
}
