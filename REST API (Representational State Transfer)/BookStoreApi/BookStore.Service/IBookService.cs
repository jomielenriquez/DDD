using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;

namespace BookStore.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book> AddAsync(Book book);
        Task DeleteAsync(Book book);
        Task UpdateAsync(Book book);
        Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page = 1, int pageSize = 10);
    }
}
