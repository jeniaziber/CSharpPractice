using MyBookApi.DataAccess.Repositories;
using MyBookApi.Models;

namespace MyBookApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Author>> GetAllAsync() 
            => _repository.GetAllAsync();

        public Task<Author?> GetByIdAsync(int id) 
            => _repository.GetByIdAsync(id);

        // Реализация добавления
        public Task AddAsync(Author author) 
            => _repository.AddAsync(author);

        // Реализация обновления
        public Task UpdateAsync(Author author) 
            => _repository.UpdateAsync(author);

        // Реализация удаления
        public Task DeleteAsync(int id) 
            => _repository.DeleteAsync(id);
    }

}