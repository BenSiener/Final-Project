using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using Final_Project.Data;


namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeamMemberController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TeamMember
        [HttpGet]
        public async Task<IActionResult> GetAll(int? id = null)
        {
            if (id == null || id == 0)
                return Ok(await _context.TeamMembers.Take(5).ToListAsync());

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
                return NotFound($"TeamMember with ID {id} not found.");

            return Ok(teamMember);
        }

        // POST: api/TeamMember
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamMember teamMember)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = teamMember.Id }, teamMember);
        }

        // PUT: api/TeamMember/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeamMember updatedMember)
        {
            if (id != updatedMember.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
                return NotFound($"TeamMember with ID {id} not found.");

            // Update properties
            teamMember.FullName = updatedMember.FullName;
            teamMember.Birthdate = updatedMember.Birthdate;
            teamMember.CollegeProgram = updatedMember.CollegeProgram;
            teamMember.YearInProgram = updatedMember.YearInProgram;
            teamMember.FavoriteLanguage = updatedMember.FavoriteLanguage;

            _context.Entry(teamMember).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TeamMember/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
                return NotFound($"TeamMember with ID {id} not found.");

            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

