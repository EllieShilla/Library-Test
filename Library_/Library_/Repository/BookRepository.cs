using AutoMapper;
using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<int> Create(BookForSaveDTO book)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BookForSaveDTO, Book>());
            var mapper = new Mapper(config);
            var bookForSave = mapper.Map<BookForSaveDTO, Book>(book);

            _context.Books.Add(bookForSave);
            await _context.SaveChangesAsync();
            return bookForSave.Id;
        }

        public async void Delete(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(item=>item.Id==id);
            if(book!=null)
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllOrderByAuthor()
        {
            return await _context.Books.OrderBy(item=>item.Author).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllOrderByTitle()
        {
            return await _context.Books.OrderBy(item => item.Title).ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<Book>> GetTop10()
        {
            return await _context.Books.Where(item => item.Reviews.Count > 10).ToListAsync();

        }

        public async Task<IEnumerable<Book>> GetTop10WithGenre(string genre)
        {
            return await _context.Books.Where(item=>item.Genre.ToLower().Equals(genre.ToLower()) && item.Reviews.Count>10).ToListAsync();
        }

        public async Task<int> Update(BookForSaveDTO book)
        {
            Book bookForUpdate=_context.Books.FirstOrDefault(item=>item.Id== book.Id);
            bookForUpdate.Title=book.Title;
            bookForUpdate.Author=book.Author;
            bookForUpdate.Cover=book.Cover;
            bookForUpdate.Content=book.Content;
            bookForUpdate.Genre=book.Genre;          

            await _context.SaveChangesAsync();
            return bookForUpdate.Id;
        }
    }
}
