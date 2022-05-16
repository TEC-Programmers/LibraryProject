﻿using LibraryProject.API.DTO_s;
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
        Task<BookResponse> CreateBook(BookRequest newBook);
        Task<BookResponse> UpdateBook(int BookId, BookRequest updateBook);
        Task<BookResponse> DeleteBook(int bookId);
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


        public async Task<BookResponse> CreateBook(BookRequest newBook)
        {
            Book book = MapBookRequestToBook(newBook);

            Book insertedBook = await _bookRepository.InsertNewBook(book);

            if (insertedBook != null)
            {
                insertedBook.Category = await _categoryRepository.SelectCategoryById(insertedBook.CategoryId);
                return MapBookToBookResponse(insertedBook);
            }

            return null;
        }

        public async Task<BookResponse> UpdateBook(int bookId, BookRequest updateBook)
        {
            Book book = MapBookRequestToBook(updateBook);

            Book updatedBook = await _bookRepository.UpdateExistingBook(bookId, book);

            if (updatedBook != null)
            {
                updatedBook.Category = await _categoryRepository.SelectCategoryById(updatedBook.CategoryId);
                return MapBookToBookResponse(updatedBook);
            }

            return null;
        }

        public async Task<BookResponse> DeleteBook(int bookId)
        {
           Book book = await _bookRepository.DeleteBookById(bookId);

            if (book != null)
            {
                book.Category = await _categoryRepository.SelectCategoryById(book.CategoryId);
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
                PublishYear = bookRequest.PublishYear,
                CategoryId = bookRequest.CategoryId,
                AuthorId= bookRequest.AuthorId
            };
        }
        private static BookResponse MapBookToBookResponse(Book book)
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
