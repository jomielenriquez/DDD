using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using BookStore.Data.Repository;

namespace BookStore.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<Book> AddAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(Guid id)
        {
            return _bookRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
