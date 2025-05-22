using Microsoft.EntityFrameworkCore;
using MyBookApi.Models;

namespace MyBookApi.DataAccess.Repositories;

public class EditionRepository : IEditionRepository
{
    private readonly AppDbContext _context;

    public EditionRepository(AppDbContext context)
    {
        _context = context;
    }

    // Получить все издания
    public async Task<IEnumerable<Edition>> GetAllAsync()
    {
        return await _context.Editions.ToListAsync();
    }

    // Получить издание по ID
    public async Task<Edition?> GetByIdAsync(int id)
    {
        return await _context.Editions.FindAsync(id);
    }
    
    // Получить все издания конкретной книги
    public async Task<IEnumerable<Edition>> GetAllByBookIdAsync(int bookId)
    {
        return await _context.Editions
            .Where(e => e.BookId == bookId)
            .ToListAsync();
    }

    // Добавить новое издание
    public async Task AddAsync(Edition edition)
    {
        _context.Editions.Add(edition);
        await _context.SaveChangesAsync();
    }

    // Обновить издание
    public async Task UpdateAsync(Edition edition)
    {
        var existing = await _context.Editions.FindAsync(edition.Id);
        //if (existing == null) return;

        existing.Format = edition.Format;
        existing.ReleaseDate = edition.ReleaseDate;
        existing.BookId = edition.BookId;

        await _context.SaveChangesAsync();
    }

    // Удалить издание
    public async Task DeleteAsync(int id)
    {
        var edition = await _context.Editions.FindAsync(id);
        //if (edition == null) return;

        _context.Editions.Remove(edition);
        await _context.SaveChangesAsync();
    }
}