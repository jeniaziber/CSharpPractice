// IEditionService.cs

using MyBookApi.Models;

namespace MyBookApi.Services
{
    public interface IEditionService
    {
        Task<IEnumerable<Edition>> GetAllAsync();
        Task<Edition?> GetByIdAsync(int id);
        Task AddAsync(Edition edition);
        Task UpdateAsync(Edition edition);
        Task DeleteAsync(int id);

        // Метод для получения всех изданий конкретной книги
        Task<IEnumerable<Edition>> GetEditionsByBookIdAsync(int bookId);
    }
}