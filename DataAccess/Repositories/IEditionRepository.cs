using MyBookApi.Models;

namespace MyBookApi.DataAccess.Repositories
{
    public interface IEditionRepository
    {
        Task<IEnumerable<Edition>> GetAllAsync();
        Task<Edition?> GetByIdAsync(int id);
        Task AddAsync(Edition edition);
        Task UpdateAsync(Edition edition);
        Task DeleteAsync(int id);
        Task<IEnumerable<Edition>> GetAllByBookIdAsync(int bookId);
    }
}
