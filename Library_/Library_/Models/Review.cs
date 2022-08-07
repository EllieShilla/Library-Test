namespace Library_.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string Reviwer { get; set; }
        public string Message { get; set; }
    }
}
