using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using Final_Project.Data;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VideoGamesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/VideoGames
        [HttpGet]
        public async Task<IActionResult> GetAll(int? id = null)
        {
            if (id == null || id == 0)
                return Ok(await _context.VideoGames.Take(5).ToListAsync());

            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
                return NotFound($"VideoGame with ID {id} not found.");

            return Ok(game);
        }

        // POST: api/VideoGames
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VideoGame game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.VideoGames.AddAsync(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = game.Id }, game);
        }

        // PUT: api/VideoGames/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VideoGame updatedGame)
        {
            if (id != updatedGame.Id)
                return BadRequest("ID mismatch.");

            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
                return NotFound($"VideoGame with ID {id} not found.");

            game.Title = updatedGame.Title;
            game.Genre = updatedGame.Genre;
            game.ReleaseDate = updatedGame.ReleaseDate;
            game.Platform = updatedGame.Platform;
            game.Rating = updatedGame.Rating;

            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VideoGames/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game == null)
                return NotFound($"VideoGame with ID {id} not found.");

            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
