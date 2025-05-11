using MyBookApi.Models;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);

    Task AddEditionAsync(Edition edition);
    Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId);
}