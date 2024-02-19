using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BookAPI.Controllers
{
    [ApiController]
    //  [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private ApplicationContext _context;
        public AuthorsController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetAuthors")]
        public List<Author> Get()
        {
            return _context.Authors.Include(p => p.Pseudonyms).Include(b => b.Books).ToList();
        }

        [HttpPost(Name = "AddAuthors")]
        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        [HttpPut(Name = "EditAuthors")]
        public void Edit(Author author)
        { 
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        //[HttpPatch]

        [HttpDelete(Name = "DeleteAuthors")]
        public void Delete(int authorId)
        {
            var deleteAuthor = _context.Authors.FirstOrDefault(e => e.Id == authorId); //Maybe
            _context.Authors.Remove(deleteAuthor);
            _context.SaveChanges();
        }

    }

}
