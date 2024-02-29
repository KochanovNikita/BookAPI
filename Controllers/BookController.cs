using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BookAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private ApplicationContext _context;
        public BookController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetBooks")]
        //[SwaggerOperation(Summary = "Get a list of books", Description = "Returns a list of books")]
        //[SwaggerResponse(200, "Successful operation", typeof(IEnumerable<Book>))]
        public ActionResult<IEnumerable<Book>> Get()
        {
            try
            {
                return Ok(_context.Books.Include(p => p.Publisher).Include(a => a.Author).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting books: {ex.Message}");
            }
        }

        [HttpPost(Name = "AddBooks")]
        public async Task<ActionResult> Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = book.Id }, book); //Google it
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding book: {ex.Message}");
            }
        }

        [HttpPut(Name = "EditBooks")]
        public async Task<ActionResult> EditAsync(int id, Book updatedBook)
        {
            if (!_context.Books.Any(b => b.Id == id))
            {
                return NotFound($"No book found with ID {id}.");
            }

            _context.Entry(updatedBook).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error editing book: {ex.Message}");
            }

            return NoContent();
        }

        [HttpDelete(Name = "DeleteBooks")]
        public ActionResult Delete(int id)
        {
            Book? bookToDelete = _context.Books.FirstOrDefault(b => b.Id == id);
            if (bookToDelete == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            try
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting book: {ex.Message}");
            }
        }
    }
}
