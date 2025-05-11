using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBookApi.Models;
using MyBookApi.Services;

namespace BookShelf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IEditionService _editionService;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService,
            IEditionService editionService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _editionService = editionService;
        }

        // GET api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Book book)
        {
            // Проверка существования автора
            var author = await _authorService.GetByIdAsync(book.AuthorId);
            if (author == null)
                return BadRequest("Автор с таким ID не существует");

            await _bookService.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        // PUT api/books/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest("ID в URL и теле не совпадают");

            var existing = await _bookService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _bookService.UpdateAsync(book);
            return NoContent();
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _bookService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        // POST api/books/{bookId}/add-edition
        [HttpPost("{bookId}/add-edition")]
        public async Task<ActionResult> AddEdition(int bookId, [FromBody] Edition edition)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
                return NotFound("Книга не найдена");

            edition.BookId = bookId;
            await _editionService.AddAsync(edition);

            // Возвращаем созданное издание через контроллер Editions
            return CreatedAtAction(
                actionName: "GetById",
                controllerName: "Editions",
                routeValues: new { id = edition.Id },
                value: edition
            );
        }

        // GET api/books/{bookId}/editions
        [HttpGet("{bookId}/editions")]
        public async Task<ActionResult<IEnumerable<Edition>>> GetAllEditionsByBookId(int bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
                return NotFound("Книга не найдена");

            var editions = await _editionService.GetEditionsByBookIdAsync(bookId);
            return Ok(editions);
        }
    }
}
