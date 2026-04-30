using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Price)
                  .HasColumnType("decimal(18,2)");
        });
    }
}
