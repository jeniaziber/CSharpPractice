namespace MyBookApi.DataAccess;

using MyBookApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Edition> Editions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связь между Автором и Книгами
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        // Связь между Книгой и Изданиями (одна книга может иметь много изданий)
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Editions)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.BookId);
    }
}