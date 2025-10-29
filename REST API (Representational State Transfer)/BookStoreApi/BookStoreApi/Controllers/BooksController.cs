using BookStoreApi.Model;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Repository;
using BookStore.Data.Context;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _bookRepository.GetAllAsync();

            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author
            };
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
            return Ok(book);
        }
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, CreateBookDto updatedBook)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
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
