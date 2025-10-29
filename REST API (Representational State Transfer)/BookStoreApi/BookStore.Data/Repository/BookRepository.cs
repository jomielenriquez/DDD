using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        => await _context.Books.ToListAsync();

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksAsync(string? search, string? sortBy, int page = 1, int pageSize = 10)
        {
            var query = _context.Books.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(search))
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));

            // Sorting
            query = sortBy switch
            {
                "title" => query.OrderBy(b => b.Title),
                "author" => query.OrderBy(b => b.Author),
                _ => query.OrderBy(b => b.BookId)
            };

            // Pagination
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


    }
}
