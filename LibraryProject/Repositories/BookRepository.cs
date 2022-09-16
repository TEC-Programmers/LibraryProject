using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    //Creating Interface of IBookRepository
    public interface IBookRepository
    {
        Task<List<Book>> SelectAllBooks();
        Task<Book> SelectBookById(int bookId);
        Task<List<Book>> SelectAllBooksByCategoryId(int categoryId);
        Task<Book> InsertNewBook(Book book);
        Task<Book> UpdateExistingBook(int bookId, Book book);
        Task<Book> DeleteBookById(int bookId);
    }
    public class BookRepository : IBookRepository   // This class is inheriting interfcae IBookRepository and implement the interfaces
    {

        private readonly LibraryProjectContext _context;   //making an instance of the class LibraryProjectContext

        public BookRepository(LibraryProjectContext context)   //dependency injection with parameter 
        {
            _context = context;
        }

        //** implementing the methods of IBookRepository interface  **// 

        //This method will get all of the books information with category, author, publisher details
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
        //This method will get one specific book info whoose bookId has been given including Category, author, publisher details
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
        //This method will get all the books info by specific categoryId including category, author, publisher details
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
        //With this method one book's info can be added
        public async Task<Book> InsertNewBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            Book insertBook = await SelectBookById(book.Id);

            return insertBook;
        }
        //Using this method existing book info can be updated by giving specific bookId
        public async Task<Book> UpdateExistingBook(int bookId, Book book)
        {
            Book updateBook = await _context.Book.FirstOrDefaultAsync(book => book.Id == bookId);

            if (updateBook != null)
            {
                updateBook.Title = book.Title;
                updateBook.Description = book.Description;
                updateBook.Language = book.Language;
                updateBook.Image    =book.Image;
                updateBook.PublishYear = book.PublishYear;
                

                await _context.SaveChangesAsync();

            }
            return updateBook;
        }
        //This method will remove all the details of one specific book by bookID
        public async Task<Book> DeleteBookById(int bookId)
        {
            Book deleteBook = await _context.Book.
                FirstOrDefaultAsync(book => book.Id == bookId);

            if (deleteBook != null)
            {
                _context.Book.Remove(deleteBook);
                await _context.SaveChangesAsync();
            }
            return deleteBook;   
        }
    }
}
