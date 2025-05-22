using Microsoft.EntityFrameworkCore;
using MyBookApi.Models;

namespace MyBookApi.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        // Получить автора по ID
        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        // Получить всех авторов
        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        // Добавить нового автора
        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        // Обновить данные автора
        public async Task UpdateAsync(Author author)
        {
            var existing = await _context.Authors.FindAsync(author.Id);
            //if (existing == null) return;

            existing.Name = author.Name;
            // здесь можно обновить другие поля

            await _context.SaveChangesAsync();
        }

        // Удалить автора по ID
        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}