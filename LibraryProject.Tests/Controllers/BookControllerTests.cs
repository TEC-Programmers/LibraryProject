using LibraryProject.API.Controllers;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly BookController _bookController;
        private readonly Mock<IBookService> _mockBookService = new();
        //private readonly Mock<ICategoryService> _mockCategoryService = new();
        //private readonly Mock<IAuthorService> _mockAuthorService = new();
        //private readonly Mock<IPublisherService> _mockPublisherService = new();

        public BookControllerTests()
        {
            _bookController = new(_mockBookService.Object/*, _mockCategoryService.Object, _mockAuthorService.Object, _mockPublisherService.Object*/);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenBooksExist()
        {
            //Arrange

            List<BookResponse> books = new();




            books.Add(new()
            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,

                AuthorId = 1,

                PublisherId = 1


            });
            books.Add(new()
            {
                Id = 2,
                Title = "abbsnsjsk",
                Language = "dansk",
                Description = "Bøøøøg",
                Image = "Book2.jpg",
                PublishYear = 1996,
                CategoryId = 1,

                AuthorId = 1,

                PublisherId = 1,

            });

            _mockBookService.Setup(x => x.GetAllBooks()).ReturnsAsync(books);



            //Act
            var result = await _bookController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoBooksExists()
        {
            //Arrange
            List<BookResponse> products = new();


            _mockBookService
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(products);



            //Act
            var result = await _bookController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange



            _mockBookService
                .Setup(x => x.GetAllBooks())
                .ReturnsAsync(() => null);



            //Act
            var result = await _bookController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockBookService
              .Setup(x => x.GetAllBooks())
              .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _bookController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int bookId = 1;

            BookResponse book = new()

            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,

                AuthorId = 1,

                PublisherId = 1

            };


            _mockBookService
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(book);



            //Act
            var result = await _bookController.GetById(bookId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenBookDoesNotExist()
        {
            //Arrange
            int bookId = 1;



            _mockBookService
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(() => null);



            //Act
            var result = await _bookController.GetById(bookId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockBookService
                .Setup(x => x.GetBookById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));



            //Act
            var result = await _bookController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenBookIsSuccessfullyCreated()
        {
            //Arrange


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

            //int productId = 1;

            BookResponse bookResponse = new()
            {

                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,

                AuthorId = 1,

                PublisherId = 1


            };
            _mockBookService
                .Setup(x => x.CreateBook(It.IsAny<BookRequest>()))
                .ReturnsAsync(bookResponse);



            //Act
            var result = await _bookController.Create(newBook);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


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


            _mockBookService
                .Setup(x => x.CreateBook(It.IsAny<BookRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _bookController.Create(newBook);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenBookIsSuccessfullyUpdated()
        {
            //Arrange


            BookRequest updateBook = new()

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

            BookResponse bookResponse = new()
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
            _mockBookService
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<BookRequest>()))
                .ReturnsAsync(bookResponse);



            //Act
            var result = await _bookController.Update(bookId, updateBook);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTryingToUpdateBookWhichDoesNotExist()
        {
            //Arrange


            BookRequest updateBook = new()

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



            _mockBookService
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<BookRequest>()))
                .ReturnsAsync(() => null);



            //Act
            int bookId = 1;

            var result = await _bookController.Update(bookId, updateBook);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            BookRequest updateBook = new()

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

            _mockBookService
                .Setup(x => x.UpdateBook(It.IsAny<int>(), It.IsAny<BookRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _bookController.Update(bookId, updateBook);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenBookIsDeleted()
        {
            //Arrange
            int bookId = 1;

            BookResponse bookResponse = new()
            {

                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                Image = "Book1.jpg",
                PublishYear = 2021,
                CategoryId = 1,

                AuthorId = 1,

                PublisherId = 1

            };
            _mockBookService
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(bookResponse);



            //Act
            var result = await _bookController.Delete(bookId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteBookWhichDoesNotExist()
        {
            //Arrange

            int bookId = 1;

            _mockBookService
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _bookController.Delete(bookId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }
        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange


            int bookId = 1;

            _mockBookService
                .Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));



            //Act
            var result = await _bookController.Delete(bookId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }


    }
}
