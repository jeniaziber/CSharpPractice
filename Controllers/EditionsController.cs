using Microsoft.AspNetCore.Mvc;
using MyBookApi.Models;
using MyBookApi.Services;

namespace BookShelf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditionsController : ControllerBase
    {
        private readonly IEditionService _service;

        public EditionsController(IEditionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Edition>> GetById(int id)
        {
            var edition = await _service.GetByIdAsync(id);
            if (edition == null)
                return NotFound();

            return Ok(edition);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Edition edition)
        {
            await _service.AddAsync(edition);
            return CreatedAtAction(nameof(GetById), new { id = edition.Id }, edition);  // Ссылаемся на GetById
        }

        // Другие методы...
    }
}
