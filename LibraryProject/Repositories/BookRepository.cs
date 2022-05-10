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
    }
}
