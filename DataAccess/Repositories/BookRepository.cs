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

    // Получить все книги
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.Editions)
            .ToListAsync();
    }

    // Получить книгу по ID вместе с изданиями
    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Editions)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    // Добавить новую книгу
    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    // Обновить книгу
    public async Task UpdateAsync(Book book)
    {
        var existing = await _context.Books
            .Include(b => b.Editions)
            .FirstOrDefaultAsync(b => b.Id == book.Id);
        if (existing == null) return;

        existing.Title = book.Title;
        existing.AuthorId = book.AuthorId;
        existing.Year = book.Year;
        // Если нужно обновить издания, это лучше делать через отдельный репозиторий

        await _context.SaveChangesAsync();
    }

    // Удалить книгу
    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    // Получить все книги конкретного автора
    public async Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.Editions)
            .ToListAsync();
    }
}