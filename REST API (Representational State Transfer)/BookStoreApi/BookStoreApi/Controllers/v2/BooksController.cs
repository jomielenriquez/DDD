using AutoMapper;
using BookStore.Data.Repository;
using BookStoreApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers.v2
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _bookRepository.GetBooksAsync(search, sortBy, page, pageSize);
            var booksDto = _mapper.Map<IEnumerable<CreateBookDto>>(books);
            return Ok(books);
        }
    }
}
