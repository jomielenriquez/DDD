using BookStoreApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                new Book { Id = 3, Title = "1984", Author = "George Orwell" }
            };

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = Books;

            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto book)
        {
            var newBook = new Book
            {
                Id = Books.Max(b => b.Id) + 1,
                Title = book.Title,
                Author = book.Author
            };
            Books.Add(newBook);
            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CreateBookDto updatedBook)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            Books.Remove(book);
            return NoContent();
        }
    }
}
