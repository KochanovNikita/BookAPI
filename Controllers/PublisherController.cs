using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        public List<Publisher> Get()
        {
            return _context.Publishers.ToList();
        }

        [HttpPost(Name = "AddPublishers")]
        public void Post(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }

        [HttpPut(Name = "EditPublishers")]
        public void Put(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
        }

        [HttpDelete(Name = "EditPublishers")]
        public void Delete(int id)
        {
            Publisher? deletePublisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            _context.Publishers.Remove(deletePublisher);
            _context.SaveChanges();
        }
    }
}
