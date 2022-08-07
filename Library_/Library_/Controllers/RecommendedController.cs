using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendedController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        public RecommendedController(IBookRepository bookRepository, IReviewRepository reviewRepository, IRatingRepository ratingRepository)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
            _ratingRepository = ratingRepository;
        }

        [HttpGet("{genre?}")]
        public async Task<IEnumerable<BooksDTO>> RecommendedBook([FromQuery] string? genre)
        {
            List<BooksDTO> books = new List<BooksDTO>();

            if (genre == null)
            {
                foreach (var item in await _bookRepository.GetTop10())
                {
                    List<Review> reviews = new List<Review>(await _reviewRepository.GetByBookId(item.Id));
                    List<Rating> ratings = new List<Rating>(await _ratingRepository.GetByBookId(item.Id));
                    int sum = 0;

                    foreach (Rating rating in ratings)
                        sum += rating.Score;

                    decimal score = 0;
                    if (sum > 0)
                        score = sum / ratings.Count;
                    BooksDTO booksDTO = new BooksDTO() { Id = item.Id, Title = item.Title, Author = item.Author, Rating = score, reviewNumber = reviews.Count, Cover=item.Cover };
                    books.Add(booksDTO);
                }
            }
            else
            {
                foreach (var item in await _bookRepository.GetTop10WithGenre(genre))
                {
                    List<Review> reviews = new List<Review>(await _reviewRepository.GetByBookId(item.Id));
                    List<Rating> ratings = new List<Rating>(await _ratingRepository.GetByBookId(item.Id));
                    int sum = 0;

                    foreach (Rating rating in ratings)
                        sum += rating.Score;

                    decimal score = 0;
                    if (sum > 0)
                        score = sum / ratings.Count;
                    BooksDTO booksDTO = new BooksDTO() { Id = item.Id, Title = item.Title, Author = item.Author, Rating = score, reviewNumber = reviews.Count, Cover = item.Cover };
                    books.Add(booksDTO);
                }
            }
            return books.OrderByDescending(item=>item.Rating).Take(10);
        }
    }
}
