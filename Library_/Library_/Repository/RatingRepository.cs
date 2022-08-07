using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LibraryContext _context;
        public RatingRepository(LibraryContext context)
        {
            _context = context;
        }

        public async void Create(int bookId, RateDTO rate)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(item => item.Id == bookId);

                Rating rating = new Rating() { BookId = bookId, Score = rate.Score };
                await _context.Ratings.AddAsync(rating);
                await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Rating>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rating>> GetByBookId(int Id)
        {
            return await _context.Ratings.Where(item => item.Book.Id == Id).ToListAsync();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Rating rating)
        {
            throw new NotImplementedException();
        }
    }
}
