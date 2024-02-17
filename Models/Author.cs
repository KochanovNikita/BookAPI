namespace BookAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }  
        public DateTime DieDate { get; set; }
        public ICollection<Pseudonym> Pseudonyms { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
