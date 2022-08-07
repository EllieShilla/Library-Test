using Library_.DTO;
using Library_.Models;

namespace Library_.Interface
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAll();
        Task<IEnumerable<Rating>> GetByBookId(int Id);
        void Create(int bookId, RateDTO rate);
        bool Save();
    }
}
