using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        Task<Book> InsertNewBookWithProcedure(Book book);
        Task<Book> InsertNewBook(Book book);
        Task<Book> UpdateExistingBookWithProcedure(int bookId, Book book);
        Task<Book> DeleteBookByIdWithProcedure(int bookId);
        Task<Book> Delete(int bookId);

    }
    public class BookRepository : IBookRepository   // This class is inheriting interfcae IBookRepository and implement the interfaces
    {

        private readonly LibraryProjectContext _context;   //making an instance of the class LibraryProjectContext

        public BookRepository(LibraryProjectContext context)   //dependency injection with parameter 
        {
            _context = context;
        }

        //implementing the methods of IBookRepository interface 
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
        public async Task<Book> InsertNewBookWithProcedure(Book book)
        {
            var Title = new SqlParameter("@Title", book.Title);
            var Language = new SqlParameter("@Language", book.Language);
            var Description = new SqlParameter("@Description", book.Description);
            var Image = new SqlParameter("@Image", book.Image);
            var PublishYear = new SqlParameter("@PublishYear", book.PublishYear);
            var CategoryId = new SqlParameter("@CategoryId", book.CategoryId);
            var AuthorId = new SqlParameter("@AuthorId", book.AuthorId);
            var PublisherId = new SqlParameter("@PublisherId", book.PublisherId);


            await _context.Database.ExecuteSqlRawAsync("exec insertBook " +
                "@Title, @Language, @Description, @Image, @PublishYear, @CategoryId, @AuthorId, @PublisherId",
                Title, Language, Description, Image, PublishYear, CategoryId, AuthorId, PublisherId);

            return book;            
        }
        public async Task<Book> InsertNewBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateExistingBookWithProcedure(int bookId, Book book)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", bookId),
               new SqlParameter("@Title", book.Title),
               new SqlParameter("@Description", book.Description),
               new SqlParameter("@Language", book.Language),
               new SqlParameter("@Image", book.Image),
               new SqlParameter("@PublishYear", book.PublishYear),
               new SqlParameter("@CategoryId", book.CategoryId),
               new SqlParameter("@AuthorId", book.AuthorId),
               new SqlParameter("@PublisherId", book.PublisherId),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateBook @Id, @Title, @Description, @Language, @Image, @PublishYear, @CategoryId, @AuthorId, @PublisherId", parameters.ToArray());
            return book;
        }
        public async Task<Book> Update(int bookId, Book book)
        {
            Book upateBook = await _context.Book
               .FirstOrDefaultAsync(book => book.Id == bookId);

            if (upateBook != null)
            {
                upateBook.Title = book.Title;
                upateBook.Language = book.Language;
                upateBook.Description = book.Description;
                upateBook.Image = book.Image;
                upateBook.PublishYear = book.PublishYear;
                upateBook.CategoryId = book.CategoryId;
                upateBook.AuthorId = book.AuthorId;
                upateBook.PublisherId = book.PublisherId;
                await _context.SaveChangesAsync();
            }

            return upateBook;

            //var parameters = new List<SqlParameter>
            //{
            //   new SqlParameter("@Id", bookId),
            //   new SqlParameter("@Title", book.Title),
            //   new SqlParameter("@Description", book.Description),
            //   new SqlParameter("@Language", book.Language),
            //   new SqlParameter("@Image", book.Image),
            //   new SqlParameter("@PublishYear", book.PublishYear),
            //   new SqlParameter("@CategoryId", book.CategoryId),
            //   new SqlParameter("@AuthorId", book.AuthorId),
            //   new SqlParameter("@PublisherId", book.PublisherId),
            //};

            //await _context.Database.ExecuteSqlRawAsync("EXEC updateBook @Id, @Title, @Description, @Language, @Image, @PublishYear, @CategoryId, @AuthorId, @PublisherId", parameters.ToArray());
            //return book;
        }

        public async Task<Book> DeleteBookByIdWithProcedure(int bookId)
        {          
            Book deletebook = await _context.Book
               .FirstOrDefaultAsync(u => u.Id == bookId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", bookId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteBook @Id", parameter.ToArray());
            return deletebook;
        }
        public async Task<Book> Delete(int bookId)
        {
            Book deletedBook = await _context.Book
                .FirstOrDefaultAsync(book => book.Id == bookId);
            if (deletedBook != null)
            {
                _context.Book.Remove(deletedBook);
                await _context.SaveChangesAsync();
            }
            return deletedBook;
        }

    }
}
