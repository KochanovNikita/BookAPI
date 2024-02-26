using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private ApplicationContext _context;
        public PublisherController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetPublishers")] //Return JSON *all*
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            try
            {
                return Ok(_context.Publishers.Include(b => b.Books).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting publishers: {ex.Message}");
            }
        }

        [HttpPost(Name = "AddPublishers")]
        public async Task<ActionResult> Add(Publisher publisher)
        {
            try
            {
                _context.Publishers.Add(publisher);
                _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = publisher.Id }, publisher); //Google it
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding publisher: {ex.Message}");
            }
        }

        [HttpPut(Name = "EditPublishers")]
        public async Task<ActionResult> EditAsync(int id, Publisher updatedPublisher)
        {
            if (!_context.Publishers.Any(b => b.Id == id))
            {
                return NotFound($"No publisher found with ID {id}.");
            }

            _context.Entry(updatedPublisher).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error editing publisher: {ex.Message}");
            }

            return NoContent();
        }

        [HttpDelete(Name = "DeletePublishers")]
        public ActionResult Delete(int id)
        {
            Publisher? publisherToDelete = _context.Publishers.FirstOrDefault(b => b.Id == id);
            if (publisherToDelete == null)
            {
                return NotFound($"Publisher with ID {id} not found.");
            }

            try
            {
                _context.Publishers.Remove(publisherToDelete);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting publisher: {ex.Message}");
            }
        }
    }
}
