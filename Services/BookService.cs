using MyBookApi.DataAccess.Repositories;
using MyBookApi.Models;

namespace MyBookApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IEditionRepository _editionRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository repository, IEditionRepository editionRepository, IAuthorRepository authorRepository)
    {
        _repository = repository;
        _editionRepository = editionRepository;
        _authorRepository = authorRepository;
    }

    public Task<IEnumerable<Book>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Book?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task AddAsync(Book book) => _repository.AddAsync(book);
    public Task UpdateAsync(Book book) => _repository.UpdateAsync(book);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

    public async Task AddEditionAsync(Edition edition)
    {
        await _editionRepository.AddAsync(edition);
    }
    
    public Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId)
        => _repository.GetAllByAuthorIdAsync(authorId);
}