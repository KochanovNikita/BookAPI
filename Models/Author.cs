using System.ComponentModel;

namespace BookAPI.Models
{
    public class Author
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }  
        public DateTime? DieDate { get; set; }
        //[DefaultValue(null)]
        public ICollection<Pseudonym>? Pseudonyms { get; private set; }
        public ICollection<Book>? Books { get; private set; }
    }
}
