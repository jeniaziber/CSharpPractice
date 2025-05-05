using Microsoft.EntityFrameworkCore;
using MyBookApi.Models;

namespace MyBookApi.DataAccess.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        var existing = await _context.Books.FindAsync(book.Id);
        if (existing == null) return;

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Year = book.Year;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}