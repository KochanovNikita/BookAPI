﻿namespace BookAPI.Models
{
    public class Book
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
    }
}
