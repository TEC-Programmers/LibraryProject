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
        Task<List<Author>> SelectAllAuthors();
        Task<Author> SelectAuthorById(int authorId);
        Task<Author> InsertNewAuthorWithProcedure(Author author);
        Task<Author> UpdateExistingAuthor(int authorId, Author author);
        Task<Author> DeleteAuthorWithProcedure(int authorId);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;

        public AuthorRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
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
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertAuthor", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FirstName", author.FirstName));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", author.MiddleName));
            cmd.Parameters.Add(new SqlParameter("@LastName", author.LastName));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return author;
        }

        public async Task<List<Author>> SelectAllAuthors()
        {
            return await _context.Author.ToListAsync();
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
    }
}
