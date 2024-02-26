using BookAPI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace BookAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private ApplicationContext _context;
        public AuthorController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetAuthors")]
        public ActionResult<IEnumerable<Author>> Get()
        {
            try 
            {
                return Ok(_context.Authors.Include(p => p.Pseudonyms).Include(b => b.Books).ToList());
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpPost(Name = "AddAuthors")]
        public async Task<IActionResult> Add([FromBody] Author author)
        {
            try
            {
                //Skip Books and Pseudonyms
                if (author.Books != null && !author.Books.Any())
                {
                    author.Books = null;
                }
                if (author.Pseudonyms != null && !author.Pseudonyms.Any())
                {
                    author.Pseudonyms = null;
                }

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { Status = "Success", Id = author.Id });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error adding author: {e.Message}");
            }
        }

        [HttpPut(Name = "EditAuthors")]
        public void Edit(Author author)
        { 
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        //POST only Author(no Books and Pseudonyms) 

        /*[HttpPatch(Name = "PatchAuthors")]
        public IActionResult Patch([FromBody] Author patchRequest)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == patchRequest.Id);
            if (author == null)
            {
                return FromResult(NotFound());
            }
            else
            {
                author.Name = patchRequest.Name;
                author.Books = patchRequest.Books;
                author.DieDate = patchRequest.DieDate;
                author.Birthday = patchRequest.Birthday;
                author.Pseudonyms = patchRequest.Pseudonyms;
                await _context.SaveChangesAsync();
                return FromResult(NoContent());
            }
        }*/

        [HttpDelete(Name = "DeleteAuthors")]
        public async Task<IActionResult> Delete(int id)
        {            
            Author? deleteAuthor = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (deleteAuthor != null)
            {
                try 
                {
                    _context.Authors.Remove(deleteAuthor);
                    _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { Status = "Success", Message = $"Author id {id} deleted." });
                }
                catch (Exception e) 
                {
                    return StatusCode(500, $"Error adding author: {e.Message}");
                }

            }            
            else
            {
                return CreatedAtAction(nameof(Get), new { Status = "Error", Message = $"Author id {id} not found." });
            }
        }

    }

}
