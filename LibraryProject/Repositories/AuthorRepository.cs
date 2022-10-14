using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> SelectAllAuthorsWithProcedure();
        Task<Author> SelectAuthorById(int authorId);
        Task<Author> InsertNewAuthorWithProcedure(Author author);
        Task<Author> InsertNewAuthor(Author author);
        Task<Author> UpdateExistingAuthor(int authorId, Author author);
        Task<Author> DeleteAuthorWithProcedure(int authorId);
        Task<Author> DeleteAuthor(int authorId);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;

        
        public AuthorRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Author> DeleteAuthor(int authorId)
        {
            Author deleteAuthor = await _context.Author
                .FirstOrDefaultAsync(author => author.Id == authorId);
            if (deleteAuthor != null)
            {
                _context.Author.Remove(deleteAuthor);
                await _context.SaveChangesAsync();
            }
            return deleteAuthor;
        }
        public async Task<Author> DeleteAuthorWithProcedure(int authorId)
        {           
            Author deleteAuthor = await _context.Author
                .FirstOrDefaultAsync(u => u.Id == authorId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", authorId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteAuthor @Id", parameter.ToArray());
            return deleteAuthor;
        }
        public async Task<Author> InsertNewAuthorWithProcedure(Author author)
        {
            var firstName = new SqlParameter("@FirstName", author.FirstName);
            var middleName = new SqlParameter("@MiddleName", author.MiddleName);
            var lastName = new SqlParameter("@LastName", author.LastName);

            await _context.Database.ExecuteSqlRawAsync("exec insertAuthor @FirstName, @MiddleName, @LastName", firstName, middleName, lastName);
            return author;          
        }
        public async Task<List<Author>> SelectAllAuthorsWithProcedure()
        {
            return await _context.Author.FromSqlRaw("selectAllAuthors").ToListAsync();
        }
        public async Task<Author> SelectAuthorById(int authorId)
        {
            return await _context.Author
                .FirstOrDefaultAsync(author => author.Id == authorId);
        }
        public async Task<Author> UpdateExistingAuthor(int authorId, Author author)
        {
            Author upateAuthor = await _context.Author
                .FirstOrDefaultAsync(author => author.Id == authorId);

            if (upateAuthor != null)
            {
                upateAuthor.FirstName = author.FirstName;
                upateAuthor.LastName = author.LastName;
                upateAuthor.MiddleName = author.MiddleName;
                await _context.SaveChangesAsync();
            }

            return upateAuthor;
        }
        public async Task<Author> InsertNewAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }
    }
}
