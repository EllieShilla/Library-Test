using Microsoft.EntityFrameworkCore;

namespace Library_.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
           : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}
