using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{

    public interface IBookRepository
    {
        Task<List<Book>> SelectAllBooks();
        Task<Book> SelectBookById(int bookId);

        Task<List<Book>> SelectAllBooksByCategoryId(int categoryId);
    }
    public class BookRepository : IBookRepository
    {

        private readonly LibraryProjectContext _context;

        public BookRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> SelectAllBooks()
        {
            return await _context.Book
                .Include(a => a.Category)
                .OrderBy(a => a.CategoryId)
                .Include(b => b.Author)
                .OrderBy(b => b.AuthorId)
                .ToListAsync();
        }

        public async Task<Book> SelectBookById(int bookId)
        {
            return await _context.Book
                .Include(b => b.Category)
                .OrderBy(c=> c.CategoryId)
                .Include(b => b.Author)
                .OrderBy(b => b.AuthorId)
                .FirstOrDefaultAsync(book => book.Id == bookId);
        }

        public async Task<List<Book>> SelectAllBooksByCategoryId(int categoryId)
        {
            return await _context.Book
               .Include(a => a.Category)
               .OrderBy(a => a.CategoryId)
               .Include(b => b.Author)
               .OrderBy(b => b.AuthorId)
               .Where(a => a.CategoryId == categoryId)
               .ToListAsync();

        }
    }


}
