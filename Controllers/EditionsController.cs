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

        // GET api/editions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Edition>>> GetAll()
        {
            var editions = await _service.GetAllAsync();
            return Ok(editions);
        }

        // GET api/editions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Edition>> GetById(int id)
        {
            var edition = await _service.GetByIdAsync(id);
            if (edition == null)
                return NotFound();

            return Ok(edition);
        }

        // POST api/editions
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Edition edition)
        {
            await _service.AddAsync(edition);
            return CreatedAtAction(nameof(GetById), new { id = edition.Id }, edition);
        }

        // PUT api/editions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Edition edition)
        {
            if (id != edition.Id)
                return BadRequest("ID в URL и теле не совпадают");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(edition);
            return NoContent();
        }

        // DELETE api/editions/{id}
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
}
