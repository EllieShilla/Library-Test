namespace Library_.Models
{
    public class Rating
    {
        public long Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Score { get; set; }
    }
}
