namespace BookAPI.Models
{
    public class Pseudonym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author? Author { get; private set; }
        public int? AuthorID { get; private set; }
    }
}
