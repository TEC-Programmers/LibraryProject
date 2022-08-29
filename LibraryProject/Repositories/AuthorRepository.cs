using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    //creating Interface of IAuthorRepository
    public interface IAuthorRepository
    {
        Task<List<Author>> SelectAllAuthors();
        Task<Author> SelectAuthorById(int authorId);
        Task<Author> InsertNewAuthor(Author author);
        Task<Author> UpdateExistingAuthor(int authorId, Author author);
        Task<Author> DeleteAuthor(int authorId);
    }
    public class AuthorRepository : IAuthorRepository   // This class is inheriting interfcae IAuthorRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;   //making an instance of the class LibraryProjectContext

        public AuthorRepository(LibraryProjectContext context)  //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IAuthorRepository interface**// 

        //This method will remove one specific Author whose Id has been got
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

        //This method will add a new Author to the system
        public async Task<Author> InsertNewAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }


        //this method will get all Authors details
        public async Task<List<Author>> SelectAllAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        //this method will get info of one Author by specific ID
        public async Task<Author> SelectAuthorById(int authorId)
        {
            return await _context.Author
                .FirstOrDefaultAsync(author => author.Id == authorId);
        }

        //This method will update the information of the specific author by ID
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
