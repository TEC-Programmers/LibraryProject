using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using LibraryProject.DTO_s;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorResponse>> GetAllAuthors();
        Task<AuthorResponse> GetAuthorById(int authorId);
        Task<AuthorResponse> CreateAuthor(AuthorRequest newAuthor);
        Task<AuthorResponse> UpdateAuthor(int authorId, AuthorRequest newAuthor);
        Task<AuthorResponse> DeleteAuthor(int authorId);
    }
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IBookRepository _bookRepository;
        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<AuthorResponse> CreateAuthor(AuthorRequest newAuthor)
        {
            Author author = MapAuthorRequestToAuthor(newAuthor);

            Author insertedAuthor = await _authorRepository.InsertNewAuthor(author);

            if (insertedAuthor != null)
            {
                
                return MapAuthorToAuthorResponse(insertedAuthor);
            }
            return null;
        }

        public async Task<AuthorResponse> DeleteAuthor(int authorId)
        {
            Author deletedAuthor = await _authorRepository.DeleteAuthor(authorId);

            if (deletedAuthor != null)
            {
               
                return MapAuthorToAuthorResponse(deletedAuthor);
            }
            return null;
        }





        public async Task<List<AuthorResponse>> GetAllAuthors()
        {
            List<Author> authors = await _authorRepository.SelectAllAuthors();
            return authors.Select(author => MapAuthorToAuthorResponse(author)).ToList();
        }



        public async Task<AuthorResponse> GetAuthorById(int authorId)
        {
            Author author = await _authorRepository.SelectAuthorById(authorId);
            if (author != null)
            {
                return MapAuthorToAuthorResponse(author);
            }
            return null;
        }

        public async Task<AuthorResponse> UpdateAuthor(int authorId, AuthorRequest updateAuthor)
        {
            Author author = MapAuthorRequestToAuthor(updateAuthor);

            Author updatedAuthor = await _authorRepository.UpdateExistingAuthor(authorId, author);

            if (updatedAuthor != null)
            {
                
                return MapAuthorToAuthorResponse(updatedAuthor);
            }
            return null;
        }




        private Author MapAuthorRequestToAuthor(AuthorRequest authorRequest)
        {
            return new Author()
            {
                FirstName = authorRequest.FirstName,
                LastName = authorRequest.LastName,
                MiddleName = authorRequest.MiddleName,
            };
        }




        private AuthorResponse MapAuthorToAuthorResponse(Author author)
        {
            return new AuthorResponse()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
            };
        }
    }
}
