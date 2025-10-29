using BookStoreApi.Model;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Repository;
using BookStore.Data.Context;
using AutoMapper;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _bookRepository.GetAllAsync();
            var listDto = _mapper.Map<IEnumerable<CreateBookDto>>(list);
            return Ok(listDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newBook = _mapper.Map<Book>(book);
            await _bookRepository.AddAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.BookId }, newBook);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<Book>(book);
            return Ok(bookDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateBookDto updatedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = _bookRepository.GetByIdAsync(updatedBook.BookId).Result;
            if (book == null)
            {
                return NotFound();
            }
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            await _bookRepository.UpdateAsync(book);
            return NoContent();
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteAsync(book);
            return NoContent();
        }
    }
}
