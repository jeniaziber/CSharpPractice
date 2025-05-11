using MyBookApi.DataAccess.Repositories;
using MyBookApi.Models;

namespace MyBookApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IEditionRepository _editionRepository;

        public BookService(
            IBookRepository repository,
            IEditionRepository editionRepository)
        {
            _repository = repository;
            _editionRepository = editionRepository;
        }

        public Task<IEnumerable<Book>> GetAllAsync() 
            => _repository.GetAllAsync();

        public Task<Book?> GetByIdAsync(int id) 
            => _repository.GetByIdAsync(id);

        public Task AddAsync(Book book) 
            => _repository.AddAsync(book);

        public Task UpdateAsync(Book book) 
            => _repository.UpdateAsync(book);

        public Task DeleteAsync(int id) 
            => _repository.DeleteAsync(id);

        public Task AddEditionAsync(Edition edition)
            => _editionRepository.AddAsync(edition);

        public Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId)
            => _repository.GetAllByAuthorIdAsync(authorId);
    }
}