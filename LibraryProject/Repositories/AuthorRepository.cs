using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> SelectAllAuthors();
        Task<Author> SelectAuthorById(int authorId);
        Task<Author> InsertNewAuthor(Author author);
        Task<Author> UpdateExistingAuthor(int authorId, Author author);
        Task<Author> DeleteAuthor(int authorId);
    }
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;

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

        public async Task<Author> InsertNewAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

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
