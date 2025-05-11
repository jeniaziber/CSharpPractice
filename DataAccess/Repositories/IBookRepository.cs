using System.Collections.Generic;
using System.Threading.Tasks;
using MyBookApi.Models;

namespace MyBookApi.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью Book.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>Получить все книги вместе с их изданиями.</summary>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>Получить книгу по идентификатору вместе с её изданиями.</summary>
        Task<Book?> GetByIdAsync(int id);

        /// <summary>Добавить новую книгу.</summary>
        Task AddAsync(Book book);

        /// <summary>Обновить существующую книгу.</summary>
        Task UpdateAsync(Book book);

        /// <summary>Удалить книгу по идентификатору.</summary>
        Task DeleteAsync(int id);

        /// <summary>Получить все книги конкретного автора вместе с их изданиями.</summary>
        Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId);
    }
}