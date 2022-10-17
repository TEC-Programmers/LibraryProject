using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    //Creating Interface of IBookService
    public interface IBookService
    {
        Task<List<BookResponse>> GetAllBooks();
        Task<BookResponse> GetBookById(int bookId);
        Task<List<BookResponse>> GetBooksByCategoryId(int categoryId);
        Task<BookResponse> CreateBookWithProcedure(BookRequest newBook);
        Task<BookResponse> CreateBook(BookRequest newBook);

        Task<BookResponse> UpdateBook(int BookId, BookRequest updateBook);
        Task<BookResponse> DeleteBook(int bookId);
    }

    public class BookService: IBookService           // This class is inheriting interfcae IBookService and implement the interface
    {
        private readonly IBookRepository _bookRepository;   //making  instances of the class IBookRepository,  ICategoryRepository, IAuthorRepository, IPublisherRepository
        private readonly ICategoryRepository  _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;

      
        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository,    //dependency injection with parameter 
            IAuthorRepository auhtorRepository, IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            
            _authorRepository = auhtorRepository;

            _publisherRepository = publisherRepository;
        }

        //implementing the methods of IBookService interface 
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
        public async Task<BookResponse> CreateBookWithProcedure(BookRequest newBook)
        {
            Book book = MapBookRequestToBook(newBook);

            Book insertedBook = await _bookRepository.InsertNewBookWithProcedure(book);

            if (insertedBook != null)
            {
                insertedBook.Category = await _categoryRepository.SelectCategoryById(insertedBook.CategoryId);
                insertedBook.Author = await _authorRepository.SelectAuthorById(insertedBook.AuthorId);
                insertedBook.Publisher = await _publisherRepository.SelectPublisherById(insertedBook.PublisherId);
                return MapBookToBookResponse(insertedBook);
            }

            return null;
        }
        public async Task<BookResponse> CreateBook(BookRequest newBook)
        {
            Book book = MapBookRequestToBook(newBook);

            Book insertedBook = await _bookRepository.InsertNewBook(book);

            if (insertedBook != null)
            {
                return MapBookToBookResponse(insertedBook);
            }
            return null;
        }

        public async Task<BookResponse> UpdateBook(int bookId, BookRequest updateBook)
        {
            Book book = MapBookRequestToBook(updateBook);

            Book updatedBook = await _bookRepository.UpdateExistingBookWithProcedure(bookId, book);

            if (updatedBook != null)
            {
                updatedBook.Category = await _categoryRepository.SelectCategoryById(updatedBook.CategoryId);
                updatedBook.Author = await _authorRepository.SelectAuthorById(updatedBook.AuthorId);
                updatedBook.Publisher = await _publisherRepository.SelectPublisherById(updatedBook.PublisherId);
                return MapBookToBookResponse(updatedBook);
            }

            return null;
        }
        public async Task<BookResponse> DeleteBook(int bookId)
        {
           Book book = await _bookRepository.DeleteBookByIdWithProcedure(bookId);

            if (book != null)
            {
                book.Category = await _categoryRepository.SelectCategoryById(book.CategoryId);
               book.Author = await _authorRepository.SelectAuthorById(book.AuthorId);
               book.Publisher = await _publisherRepository.SelectPublisherById(book.PublisherId);
                return MapBookToBookResponse(book);
            }

            return null;
        }
        private static Book MapBookRequestToBook(BookRequest bookRequest)
        {
            return new Book()
            {

                Title = bookRequest.Title,
                Description = bookRequest.Description,
                Language = bookRequest.Language,
                Image=bookRequest.Image,
                PublishYear = bookRequest.PublishYear,
                CategoryId = bookRequest.CategoryId,
                AuthorId = bookRequest.AuthorId,
                PublisherId = bookRequest.PublisherId,
            };
        }
        private BookResponse MapBookToBookResponse(Book book)
        {

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Image = book.Image,
                Language = book.Language,
                PublishYear = book.PublishYear,

                CategoryId = book.CategoryId,
                Category = new BookCategoryResponse
                {
                    Id = book.Category.Id,
                    CategoryName = book.Category.CategoryName
                },
                AuthorId = book.AuthorId,
                Author = new BookAuthorResponse
                {
                    Id = book.AuthorId,
                    FirstName = book.Author.FirstName,
                    MiddleName = book.Author.MiddleName,
                    LastName = book.Author.LastName
                },
                PublisherId = book.PublisherId,
                Publisher = new BookPublisherResponse
                {
                    Id = book.PublisherId,
                    Name = book.Publisher.Name,               
                }
            };
        }
    }
}
