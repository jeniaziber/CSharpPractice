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

    public Task<IEnumerable<Edition>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Edition?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IEnumerable<Edition>> GetAllByBookIdAsync(int bookId)
    {
        return await _context.Editions.Where(e => e.BookId == bookId).ToListAsync();
    }

    public async Task AddAsync(Edition edition)
    {
        _context.Editions.Add(edition);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Edition edition)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    // Остальные методы для работы с изданиями
}