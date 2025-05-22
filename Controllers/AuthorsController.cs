using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBookApi.Models;
using MyBookApi.Services;

namespace MyBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IEditionService _editionService;

        public AuthorsController(
            IAuthorService authorService,
            IBookService bookService,
            IEditionService editionService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _editionService = editionService;
        }
    
        // GET api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
    
        // GET api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            //if (author == null) return NotFound();
            return Ok(author);
        }
    
        // POST api/authors
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Author author)
        {
            await _authorService.AddAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }
    
        // PUT api/authors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Author author)
        {
            if (id != author.Id) return BadRequest();
            var existing = await _authorService.GetByIdAsync(id);
            //if (existing == null) return NotFound();
    
            await _authorService.UpdateAsync(author);
            return NoContent();
        }
    
        // DELETE api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _authorService.GetByIdAsync(id);
            //if (existing == null) return NotFound();
    
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
        
        // GET api/authors/{authorId}/editions
        [HttpGet("{authorId}/editions")]
        public async Task<ActionResult<IEnumerable<Edition>>> GetAllEditionsByAuthorId(int authorId)
        {
            var author = await _authorService.GetByIdAsync(authorId);
            //if (author == null)
                //return NotFound("Автор не найден");

            // Получаем все книги этого автора
            var books = await _bookService.GetAllByAuthorIdAsync(authorId);
            //if (books == null || !books.Any())
              //  return NotFound("У автора нет книг");

            // Собираем все издания для найденных книг
            var editions = new List<Edition>();
            foreach (var book in books)
            {
                var bookEditions = await _editionService.GetEditionsByBookIdAsync(book.Id);
                editions.AddRange(bookEditions);
            }

            return Ok(editions);
        }
    }
}
