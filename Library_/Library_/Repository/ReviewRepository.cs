using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryContext _context;
        public ReviewRepository(LibraryContext context)
        {
            _context = context;
        }
        public Task<Review> Create(Review review)
        {

            throw new NotImplementedException();
        }

        public async Task<int> Create(int bookId, ReviewDTO review)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(item => item.Id == bookId);
            Review newReview = new Review() { BookId = bookId, Message = review.Message, Reviwer = review.Reviwer };
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return newReview.Id;
        }

        public Task<IEnumerable<Review>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Review>> GetByBookId(int Id)
        {
            return await _context.Reviews.Where(item => item.Book.Id == Id).ToListAsync();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
