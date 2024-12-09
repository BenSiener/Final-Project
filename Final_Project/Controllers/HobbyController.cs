using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using Final_Project.Data;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public HobbyController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Hobby
        [HttpGet]
        public async Task<IActionResult> GetAll(int? id = null)
        {
            if (id == null || id == 0)
                return Ok(await _context.Hobbies.Take(5).ToListAsync());

            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
                return NotFound($"Hobby with ID {id} not found.");

            return Ok(hobby);
        }

        // POST: api/Hobby
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Hobby hobby)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Hobbies.AddAsync(hobby);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = hobby.Id }, hobby);
        }

        // PUT: api/Hobby/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Hobby updatedHobby)
        {
            if (id != updatedHobby.Id)
                return BadRequest("ID mismatch.");

            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
                return NotFound($"Hobby with ID {id} not found.");

            hobby.Name = updatedHobby.Name;
            hobby.Description = updatedHobby.Description;

            _context.Entry(hobby).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Hobby/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
                return NotFound($"Hobby with ID {id} not found.");

            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

