using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private ApplicationContext _context;
        public BookController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetBooks")]
        public List<Book> Get()
        {
            return _context.Books.Include(p => p.Publisher).Include(a => a.Author).ToList();
        }

        [HttpPost(Name = "AddBooks")]
        public void Add(Book book) 
        {
        _context.Books.Add(book);
        _context.SaveChanges();
        }

        [HttpPut(Name = "EditBooks")]
        public void Edit(Book book) 
        {
        _context.Books.Update(book);
        _context.SaveChanges();
        }

        [HttpDelete(Name = "DeleteBooks")]
        public void Delete(int bookId) 
        {
        var deleteBook = _context.Books.FirstOrDefault(b => b.Id == bookId);
        _context.Books.Remove(deleteBook);
        _context.SaveChanges();
        }
    }
}
