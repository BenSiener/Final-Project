using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using Final_Project.Data;


namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteFoodController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FavoriteFoodController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteFood
        [HttpGet]
        public async Task<IActionResult> GetAll(int? id = null)
        {
            if (id == null || id == 0)
                return Ok(await _context.FavoriteFoods.Take(5).ToListAsync());

            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null)
                return NotFound($"FavoriteFood with ID {id} not found.");

            return Ok(food);
        }

        // POST: api/FavoriteFood
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoriteFood food)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.FavoriteFoods.AddAsync(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = food.Id }, food);
        }

        // PUT: api/FavoriteFood/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FavoriteFood updatedFood)
        {
            if (id != updatedFood.Id)
                return BadRequest("ID mismatch.");

            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null)
                return NotFound($"FavoriteFood with ID {id} not found.");

            food.Name = updatedFood.Name;
            food.CuisineType = updatedFood.CuisineType;
            food.IsVegetarian = updatedFood.IsVegetarian;
            food.Calories = updatedFood.Calories;
            food.FlavorProfile = updatedFood.FlavorProfile;

            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FavoriteFood/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null)
                return NotFound($"FavoriteFood with ID {id} not found.");

            _context.FavoriteFoods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
