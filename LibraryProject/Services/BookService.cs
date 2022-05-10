using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{

    public interface IBookService
    {
        Task<List<BookResponse>> GetAllBooks();
    }

    public class BookService:IBookService
    {
        private readonly IBookRepository _bookRepository;
        //private readonly ICategoryRepository  _categoryRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            
        }
        public async Task<List<BookResponse>> GetAllBooks()
        {
            List<Book> books = await _bookRepository.SelectAllBooks();

            return books.Select(book => MapBookToBookResponse(book)).ToList();

        }
        private BookResponse MapBookToBookResponse(Book book)
        {

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Language=book.Language,
                PublishYear = book.PublishYear,
                CategoryId = book.CategoryId,
                Category = new BookCategoryResponse
                {
                    Id = book.Category.Id,
                    CategoryName =book.Category.CategoryName

                },
                AuthorId=book.AuthorId,
                Author= new BookAuthorResponse
                {
                    Id=book.AuthorId,
                    FirstName=book.Author.FirstName,
                    MiddleName=book.Author.MiddleName,
                    LastName=book.Author.LastName

                }
            };
        }
    }
}
