using Library_.DTO;
using Library_.Models;

namespace Library_.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetByBookId(int Id);
        Task<int> Create(int bookId, ReviewDTO review);
        bool Update(Review review);
        bool Save();
    }
}
