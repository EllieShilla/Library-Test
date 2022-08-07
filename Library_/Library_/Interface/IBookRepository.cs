using Library_.DTO;
using Library_.Models;

namespace Library_.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetBookById(int id);
        public Task<IEnumerable<Book>> GetAllOrderByAuthor();
        Task<IEnumerable<Book>> GetAllOrderByTitle();
        Task<IEnumerable<Book>> GetTop10();
        Task<IEnumerable<Book>> GetTop10WithGenre(string genre);
        void Delete(int id);
        Task<int> Create(BookForSaveDTO book);
        Task<int> Update(BookForSaveDTO book);
    }
}
