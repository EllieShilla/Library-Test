using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Library_.DTO;
using Library_.Interface;
using Library_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Configuration;
using System.Net;

namespace Library_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;
        private IValidator<ReviewDTO> _reviewValidator;

        public BooksController(IBookRepository bookRepository, IReviewRepository reviewRepository, IRatingRepository ratingRepository, IConfiguration configuration, IMapper mapper, IValidator<ReviewDTO> reviewValidator)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
            _ratingRepository = ratingRepository;
            Configuration = configuration;
            _mapper = mapper;
            _reviewValidator = reviewValidator;
        }

        [HttpGet("{order?}")]
        public async Task<ActionResult<BooksDTO>> GetBooks([FromQuery(Name = "order")] string? order = "author")
        {
            List<BooksDTO> books = new List<BooksDTO>();

            if (order.Equals("author"))
            {
                foreach (var item in await _bookRepository.GetAllOrderByAuthor())
                {
                    List<Review> reviews = new List<Review>(await _reviewRepository.GetByBookId(item.Id));
                    List<Rating> ratings = new List<Rating>(await _ratingRepository.GetByBookId(item.Id));
                    decimal sum = 0;

                    foreach (Rating rating in ratings)
                        sum += rating.Score;

                    decimal score = 0;
                    if (sum > 0)
                        score = sum / ratings.Count;
                    BooksDTO booksDTO = new BooksDTO() { Id = item.Id, Title = item.Title, Author = item.Author, Rating = Math.Round(score, 1), reviewNumber = reviews.Count, Cover=item.Cover };
                    books.Add(booksDTO);
                }
            }
            else if (order.Equals("title"))
            {
                foreach (var item in await _bookRepository.GetAllOrderByTitle())
                {
                    List<Review> reviews = new List<Review>(await _reviewRepository.GetByBookId(item.Id));
                    List<Rating> ratings = new List<Rating>(await _ratingRepository.GetByBookId(item.Id));
                    decimal sum = 0;

                    foreach (Rating rating in ratings)
                        sum += rating.Score;

                    decimal score = 0;
                    if (sum > 0)
                        score = sum / ratings.Count;
                    BooksDTO booksDTO = new BooksDTO() { Id = item.Id, Title = item.Title, Author = item.Author, Rating = Math.Round(score, 1), reviewNumber = reviews.Count, Cover = item.Cover };
                    books.Add(booksDTO);
                }
            }
            Console.WriteLine(HttpStatusCode.OK);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailsDTO>> BookDetails([FromRoute] int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            BookDetailsDTO bookDetails = null;
            if (book != null)
            {
                List<Rating> ratings = new List<Rating>(await _ratingRepository.GetByBookId(id));

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewDTO>());
                var mapper = new Mapper(config);
                var _reviews = mapper.Map<IEnumerable<Review>, List<ReviewDTO>>(await _reviewRepository.GetByBookId(id));

                decimal sum = 0;

                foreach (Rating rating in ratings)
                    sum += rating.Score;

                decimal score = 0;
                if (sum > 0)
                    score = sum / ratings.Count;

                bookDetails = new BookDetailsDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Cover = book.Cover,
                    Content = book.Content,
                    Genre = book.Genre,
                    Rating = Math.Round(score, 1),
                    reviews = _reviews
                };
            }
            else
                return NotFound("Book with specified id not found");


            return Ok(bookDetails);
        }

        [HttpDelete("{id}/{secret=qwerty}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromQuery(Name = "secret")] string secret)
        {
            Book bookDetails = await _bookRepository.GetBookById(id);

            if (bookDetails != null && secret.Equals(Configuration["SecretKey"]))
            {
                _bookRepository.Delete(id);
            }
            else if (!secret.Equals(Configuration["SecretKey"]))
            {
                return BadRequest("Invalid key");
            }
            else if (bookDetails != null)
            {
                return NotFound("Book with specified id not found");
            }
            return Ok();
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<BookIdDTO>> CreateBook(BookForSaveDTO book)
        {
            BookIdDTO bookId = new BookIdDTO();
            Book bookDetails = await _bookRepository.GetBookById(book.Id);

            if (ModelState.IsValid)
            {

                if (bookDetails != null && book.Id != 0)
                {
                    bookId.Id = await _bookRepository.Update(book);
                    return Ok(bookId);
                }
                else if (bookDetails == null && book.Id == 0)
                {
                    bookId.Id = await _bookRepository.Create(book);
                    return Ok(bookId);
                }
                else if (bookDetails == null && book.Id != 0)
                    return NotFound("Book with specified id not found");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}/review")]
        public async Task<ActionResult<ReviewIdDTO>> ReviewBook([FromRoute] int id, ReviewDTO review)
        {
            ValidationResult result = await _reviewValidator.ValidateAsync(review);
            ReviewIdDTO reviewId = new ReviewIdDTO();

            if (ModelState.IsValid)
            {
                Book book = await _bookRepository.GetBookById(id);
                if (book != null)
                {
                    reviewId.Id = await _reviewRepository.Create(id, review);
                    return Ok(reviewId);

                }
                else if (book == null)
                    return NotFound("Book with specified id not found");
            }

            return BadRequest(ModelState);
        }


        [HttpPut("{id}/rate")]
        public async Task<ActionResult> RateBook([FromRoute] int id, RateDTO score)
        {
            Book book = await _bookRepository.GetBookById(id);

            if (ModelState.IsValid)
            {
                if (book != null && (score.Score > 0 && score.Score < 6))
                    _ratingRepository.Create(id, score);
                else if (book == null)
                    return NotFound("Book with specified id not found");
            }

            return BadRequest(ModelState);
        }

    }

}
