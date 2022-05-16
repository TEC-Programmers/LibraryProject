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
        Task<Book> InsertNewBook(Book product);
        Task<Book> UpdateExistingBook(int bookId, Book book);
        Task<Book> DeleteBookById(int bookId);
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
                .Include(p => p.Publisher)
                .OrderBy(p => p.PublisherId)
                .ToListAsync();
        }

        public async Task<Book> SelectBookById(int bookId)
        {
            return await _context.Book
                .Include(b => b.Category)
                .OrderBy(c => c.CategoryId)
                .Include(b => b.Author)
                .OrderBy(b => b.AuthorId)
                .Include(p => p.Publisher)
                .OrderBy(p => p.PublisherId)
                .FirstOrDefaultAsync(book => book.Id == bookId);
        }

        public async Task<List<Book>> SelectAllBooksByCategoryId(int categoryId)
        {
            return await _context.Book
               .Include(a => a.Category)
               .OrderBy(a => a.CategoryId)
               .Include(b => b.Author)
               .OrderBy(b => b.AuthorId)
               .Include(p => p.Publisher)
               .OrderBy(p => p.PublisherId)
               .Where(a => a.CategoryId == categoryId)
               .ToListAsync();

        }
        public async Task<Book> InsertNewBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateExistingBook(int bookId, Book book)
        {
            Book updateBook = await _context.Book.FirstOrDefaultAsync(book => book.Id == bookId);

            if (updateBook != null)
            {
                updateBook.Title = book.Title;
                updateBook.Description = book.Description;
                updateBook.Language = book.Language;
                updateBook.PublishYear = book.PublishYear;
                

                await _context.SaveChangesAsync();

            }
            return updateBook;
        }

        public async Task<Book> DeleteBookById(int bookId)
        {
            Book deleteBook = await _context.Book.FirstOrDefaultAsync(book => book.Id == bookId);

            if (deleteBook != null)
            {
                _context.Book.Remove(deleteBook);
                await _context.SaveChangesAsync();
            }
            return deleteBook;
        }
    }
}
