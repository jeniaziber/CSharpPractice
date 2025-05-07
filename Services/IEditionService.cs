using MyBookApi.Models;

namespace MyBookApi.Services
{
    public interface IEditionService
    {
        Task<IEnumerable<Edition>> GetAllAsync();
        Task<Edition?> GetByIdAsync(int id);
        Task AddAsync(Edition edition);  // Добавить метод для добавления
        Task UpdateAsync(Edition edition);
        Task DeleteAsync(int id);
        Task<IEnumerable<Edition>> GetEditionsByBookIdAsync(int bookId);
    }
}