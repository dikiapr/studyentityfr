using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(b => b.Author)
                  .WithMany(a => a.Books)
                  .HasForeignKey(b => b.AuthorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasIndex(a => a.Name).IsUnique();
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        DateTime baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Author[] authors = new Author[]
        {
            new Author
            {
                Id = 1,
                Name = "Robert C. Martin",
                Bio = "Software engineer and author of Clean Code and Clean Architecture.",
                CreatedAt = baseDate
            },
            new Author
            {
                Id = 2,
                Name = "Andrew Hunt",
                Bio = "Co-author of The Pragmatic Programmer.",
                CreatedAt = baseDate
            }
        };

        modelBuilder.Entity<Author>().HasData(authors);

        Book[] books = new Book[]
        {
            new Book
            {
                Id = 1,
                Title = "Clean Code",
                AuthorId = 1,
                Price = 150000m,
                Stock = 10,
                CreatedAt = baseDate.AddDays(1)
            },
            new Book
            {
                Id = 2,
                Title = "Clean Architecture",
                AuthorId = 1,
                Price = 175000m,
                Stock = 7,
                CreatedAt = baseDate.AddDays(2)
            },
            new Book
            {
                Id = 3,
                Title = "The Pragmatic Programmer",
                AuthorId = 2,
                Price = 200000m,
                Stock = 5,
                CreatedAt = baseDate.AddDays(3)
            }
        };

        modelBuilder.Entity<Book>().HasData(books);
    }
}
