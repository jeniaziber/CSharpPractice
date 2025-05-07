using MyBookApi.DataAccess.Repositories;
using MyBookApi.Models;

namespace MyBookApi.Services
{
    public class EditionService : IEditionService
    {
        private readonly IEditionRepository _repository;

        public EditionService(IEditionRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Edition>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Edition?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Edition edition) => _repository.AddAsync(edition);  // Реализация метода
        public Task UpdateAsync(Edition edition) => _repository.UpdateAsync(edition);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

        public Task<IEnumerable<Edition>> GetEditionsByBookIdAsync(int bookId)
            => _repository.GetAllByBookIdAsync(bookId);
    }
}