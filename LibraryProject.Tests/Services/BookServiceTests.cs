using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public  class BookServiceTests
    {
        private readonly BookService _bookService;

        private readonly Mock<IBookRepository> _mockBookRepository = new();
        private readonly Mock<ICategoryRepository> _mockCategoryRepository = new();
        private readonly Mock<IAuthorRepository> _mockAuthorRepository = new();
        private readonly Mock<IPublisherRepository> _mockPublisherRepository = new();

        public BookServiceTests()
        {
            _bookService = new BookService(_mockBookRepository.Object, _mockCategoryRepository.Object, _mockAuthorRepository.Object, _mockPublisherRepository.Object);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnListOfBookResponses_WhenBooksExists()
        {
            // Arrange
            List<Book> Books = new();
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };

            Books.Add(new()
            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                Category = newCategory,
                AuthorId = 1,
                Author=newAuthor,
                PublisherId = 1,
                Publisher=newPublisher

            });

            Books.Add(new()
            {
                Id = 2,
                Title = "abbsnsjsk",
                Language="dansk",
                Description = "Bøøøøg",
                Image = "Book1.jpg",
                PublishYear = 1996,
                CategoryId = 1,
                Category = newCategory,
                AuthorId = 1,
                Author = newAuthor,
                PublisherId = 1,
                Publisher = newPublisher
            });

            _mockBookRepository
                .Setup(x => x.SelectAllBooks())
                .ReturnsAsync(Books);

            // Act
            var result = await _bookService.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<BookResponse>>(result);
        }
        [Fact]
        public async void GetAllBooks_ShouldReturnEmptyListOfBookResponses_WhenNoBooksExists()
        {
            // Arrange
            List<Book> Books = new();

            _mockBookRepository
                .Setup(x => x.SelectAllBooks())
                .ReturnsAsync(Books);

            // Act
            var result = await _bookService.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<BookResponse>>(result);
        }

        [Fact]
        public async void GetBookById_ShouldReturnBookResponse_WhenBookExists()
        {
            // Arrange

            int productId = 1;
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };

            Book book = new()
            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                Category = newCategory,
                AuthorId = 1,
                Author = newAuthor,
                PublisherId = 1,
                Publisher = newPublisher

            };

            _mockBookRepository
                .Setup(x => x.SelectBookById(It.IsAny<int>()))
                .ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Language, result.Language);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(book.PublishYear, result.PublishYear);
           
            Assert.Equal(book.CategoryId, result.CategoryId);
            Assert.Equal(book.PublisherId, result.PublisherId);
            Assert.Equal(book.AuthorId, result.AuthorId);

        }


        [Fact]
        public async void GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;

            _mockBookRepository
                .Setup(x => x.SelectBookById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _bookService.GetBookById(bookId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateBook_ShouldReturnBookResponse_WhenCreateIsSuccess()
        {
            // Arrange
            
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };

            BookRequest newBook = new()
            {
                
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                AuthorId = 1,
                PublisherId = 1
               

            };

            int bookId = 1;
            

            Book createdBook = new()
            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                Category = newCategory,
                AuthorId = 1,
                Author = newAuthor,
                PublisherId = 1,
                Publisher = newPublisher
            };

            _mockBookRepository

                .Setup(x => x.InsertNewBook(It.IsAny<Book>()))
                .ReturnsAsync(createdBook);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);
            _mockAuthorRepository
                 .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(newAuthor);
            _mockPublisherRepository
                 .Setup(x => x.SelectPublisherById(It.IsAny<int>()))
                .ReturnsAsync(newPublisher);

            // Act
            var result = await _bookService.CreateBook(newBook);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(newBook.Title, result.Title);
            Assert.Equal(newBook.Language, result.Language);
            Assert.Equal(newBook.Description, result.Description);
            
            Assert.Equal(newBook.PublishYear, result.PublishYear);
            Assert.Equal(newBook.CategoryId, result.CategoryId);
            Assert.Equal(newBook.PublisherId, result.PublisherId);
            Assert.Equal(newBook.AuthorId, result.AuthorId);


        }

        [Fact]
        public async void CreateBook_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };

            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };
            BookRequest newBook = new()
            {
                Title = "Pipi Lang Strømpeer",
                Description = "Kids book",
                Language="English",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                AuthorId = 1,
                PublisherId = 1
            };

            _mockBookRepository
                .Setup(x => x.InsertNewBookWithProcedure(It.IsAny<Book>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _bookService.CreateBook(newBook);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateBook_ShouldReturnBookResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test if anything actually changed on the DB,
            // we only test that the returned values match the submitted values
            // Arrange
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };
             BookRequest bookRequest = new()
            {
                 Title = "cccccc",
                 Language = "en",
                 Description = "hdjdjd",
                 Image = "Book1.jpg",
                 PublishYear = 2021,
                 CategoryId = 1,
                 AuthorId = 1,
                 PublisherId = 1


             };

            int bookId = 1;

           Book book = new()
            {
                Id = bookId,
                Title = "cccccc",
                Language = "en",
                Description = "hdjdjd",
               Image = "Book1.jpg",
               PublishYear = 2021,
                CategoryId = 1,
                Category = null,
                AuthorId = 1,
                Author=null,
                PublisherId = 1,
                Publisher=null
            };

            _mockBookRepository
                .Setup(x => x.UpdateExistingBookWithProcedure(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(book);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);
             _mockAuthorRepository
                 .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(newAuthor);
            _mockPublisherRepository
                 .Setup(x => x.SelectPublisherById(It.IsAny<int>()))
                .ReturnsAsync(newPublisher);

            // Act
            var result = await _bookService.UpdateBook(bookId, bookRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(bookRequest.Title, result.Title);
            Assert.Equal(bookRequest.Description, result.Description);
            Assert.Equal(bookRequest.Language, result.Language);
            Assert.Equal(bookRequest.PublishYear, result.PublishYear);
            Assert.Equal(bookRequest.CategoryId, result.CategoryId);
            Assert.Equal(bookRequest.PublisherId, result.PublisherId);
            Assert.Equal(bookRequest.AuthorId, result.AuthorId);


        }


        [Fact]
        public async void UpdateBook_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            BookRequest bookRequest = new()
            {
                Title = "cccccc",
                Language = "en",
                Description = "hdjdjd",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                AuthorId = 1,
                PublisherId = 1
            };

            int bookId = 1;

            _mockBookRepository
                .Setup(x => x.UpdateExistingBookWithProcedure(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _bookService.UpdateBook(bookId, bookRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteBook_ShouldReturnBookResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int bookId = 1;

            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };
            Author newAuthor = new()
            {
                Id = 1,
                FirstName = "gyy",
                MiddleName = "ghh",
                LastName = "ffg",

            };
            Publisher newPublisher = new()
            {
                Id = 1,
                Name = "hhh"
            };


            Book deletedBook = new()
            {
                Id = bookId,
                Title = "cccccc",
                Language = "en",
                Description = "hdjdjd",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,
                Category = null,
                AuthorId = 1,
                Author = null,
                PublisherId = 1,
                Publisher = null
            };

            _mockBookRepository
                .Setup(x => x.DeleteBookByIdWithProcedure(It.IsAny<int>()))
                .ReturnsAsync(deletedBook);
            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(newCategory);

            _mockAuthorRepository
                 .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(newAuthor);
            _mockPublisherRepository
                 .Setup(x => x.SelectPublisherById(It.IsAny<int>()))
                .ReturnsAsync(newPublisher);
            // Act
            var result = await _bookService.DeleteBook(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(bookId, result.Id);

        }

        [Fact]
        public async void DeleteBook_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;

            _mockBookRepository
                .Setup(x => x.DeleteBookByIdWithProcedure(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _bookService.DeleteBook(bookId);

            // Assert
            Assert.Null(result);
        }
    }
}
