
using MyBookApi.Models;
using MyBookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book)
    {
        await _service.AddAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Book book)
    {
        if (id != book.Id)
            return BadRequest("ID в URL и теле не совпадают");

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.UpdateAsync(book);
        return NoContent(); // 204 No Content — всё ок
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);
        return NoContent();
    }
}