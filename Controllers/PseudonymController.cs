using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookAPI.Models;
using BookAPI;

namespace pseudonymAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PseudonymController: ControllerBase
    {                
            private ApplicationContext _context;
            public PseudonymController(ApplicationContext context) { _context = context; }

            [HttpGet(Name = "GetPseudonyms")] //Return JSON *all*
            public ActionResult<IEnumerable<Pseudonym>> Get()
            {
                try
                {
                    return Ok(_context.Pseudonyms.Include(a => a.Author).ToList());
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error getting pseudonyms: {ex.Message}");
                }
            }

            [HttpPost(Name = "AddPseudonyms")]
            public async Task<ActionResult> Add(Pseudonym pseudonym)
            {
                try
                {
                    _context.Pseudonyms.Add(pseudonym);
                    _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = pseudonym.Id }, pseudonym); //Google it
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error adding pseudonym: {ex.Message}");
                }
            }

            [HttpPut(Name = "EditPseudonyms")]
            public async Task<ActionResult> EditAsync(int id, Pseudonym updatedpseudonym)
            {
                if (!_context.Pseudonyms.Any(b => b.Id == id))
                {
                    return NotFound($"No pseudonym found with ID {id}.");
                }

                _context.Entry(updatedpseudonym).State = EntityState.Modified;
                try
                {
                    _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error editing pseudonym: {ex.Message}");
                }
                return NoContent();
            }

            [HttpDelete(Name = "DeletePseudonyms")]
            public ActionResult Delete(int id)
            {
                Pseudonym? pseudonymToDelete = _context.Pseudonyms.FirstOrDefault(b => b.Id == id);
                if (pseudonymToDelete == null)
                {
                    return NotFound($"Pseudonym with ID {id} not found.");
                }
                try
                {
                    _context.Pseudonyms.Remove(pseudonymToDelete);
                    _context.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error deleting pseudonym: {ex.Message}");
                }
            }
    }
}
