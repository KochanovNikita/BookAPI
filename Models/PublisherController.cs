using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookAPI.Models
{
    [ApiController]
    public class PublisherController: ControllerBase
    {
        private ApplicationContext _context;
        public PublisherController(ApplicationContext context) { _context = context; }

        [HttpGet(Name = "GetPublishers")]
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
        public void Delete(int pId) 
        {
            var deletePublisher = _context.Publishers.FirstOrDefault(p => p.Id == pId);
            _context.Publishers.Remove(deletePublisher);
            _context.SaveChanges();
        }
    }
}
