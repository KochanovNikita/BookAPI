using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PseudonymController
    {                
            private ApplicationContext _context;
            public PseudonymController(ApplicationContext context) { _context = context; }

            [HttpGet(Name = "GetPseudonyms")] //Return JSON *all*
            public List<Pseudonym> Get()
            {
                return _context.Pseudonyms.ToList();
            }

            [HttpPost(Name = "AddPseudonyms")]
            public void Post(Pseudonym pseudonym)
            {
                _context.Pseudonyms.Add(pseudonym);
                _context.SaveChanges();
            }

            [HttpPut(Name = "EditPseudonyms")]
            public void Put(Pseudonym pseudonym)
            {
                _context.Pseudonyms.Update(pseudonym);
                _context.SaveChanges();
            }

            [HttpDelete(Name = "EditPseudonyms")]
            public void Delete(int id)
            {
                Pseudonym? deletePseudonym = _context.Pseudonyms.FirstOrDefault(p => p.Id == id);
                _context.Pseudonyms.Remove(deletePseudonym);
                _context.SaveChanges();
            }        
    }
}
