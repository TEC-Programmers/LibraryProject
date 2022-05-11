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
        Task<BookResponse> GetBookById(int bookId);
        Task<List<BookResponse>> GetBooksByCategoryId(int categoryId);
    }

    public class BookService:IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository  _categoryRepository;
        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            
        }
        public async Task<List<BookResponse>> GetAllBooks()
        {
            List<Book> books = await _bookRepository.SelectAllBooks();

            return books.Select(book => MapBookToBookResponse(book)).ToList();

        }

        public async Task<BookResponse> GetBookById(int bookId)
        {
            Book book = await _bookRepository.SelectBookById(bookId);

            if (book != null)
            {

                return MapBookToBookResponse(book);
            }
            return null;

        }
        public async Task<List<BookResponse>> GetBooksByCategoryId(int categoryId)
        {
            List<Book> books = await _bookRepository.SelectAllBooksByCategoryId(categoryId);


            return books.Select(book => MapBookToBookResponse(book)).ToList();
        }
        private static Book MapBookRequestToBook(BookRequest bookRequest)
        {
            return new Book()
            {

                Title = bookRequest.Title,
                Description = bookRequest.Description,
                Language = bookRequest.Language,
                PublishYear = bookRequest.PublishYear,
                CategoryId = bookRequest.CategoryId
            };
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
