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
        Task<Book> UpdateExistingBookWithProcedure(int bookId, Book book);
        Task<Book> DeleteBookByIdWithProcedure(int bookId);
    }
    public class BookRepository : IBookRepository   // This class is inheriting interfcae IBookRepository and implement the interfaces
    {

        private readonly LibraryProjectContext _context;   //making an instance of the class LibraryProjectContext
        public string _connectionString;

        public BookRepository(LibraryProjectContext context, IConfiguration configuration)   //dependency injection with parameter 
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
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
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertBook", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Title", book.Title));
            cmd.Parameters.Add(new SqlParameter("@Language", book.Language));
            cmd.Parameters.Add(new SqlParameter("@Description", book.Description));
            cmd.Parameters.Add(new SqlParameter("@Image", book.Image));
            cmd.Parameters.Add(new SqlParameter("@PublishYear", book.PublishYear));
            cmd.Parameters.Add(new SqlParameter("@CategoryId", book.CategoryId));
            cmd.Parameters.Add(new SqlParameter("@AuthorId", book.AuthorId));
            cmd.Parameters.Add(new SqlParameter("@PublisherId", book.PublisherId));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
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
    }
}
