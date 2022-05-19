using LibraryProject.API.DTO_s;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async void GetAllProducts_ShouldReturnListOfProductResponses_WhenProductsExists()
        {
            // Arrange
            List<Book> Books = new();
            Category newCategory = new()
            {
                Id = 1,
                CategoryName = "Toy"
            };


            Books.Add(new()
            {
                Id = 1,
                Title = "ToyBus",
                Language = "English",
                Description = "Kids Toys",
                PublishYear = 2021,
                CategoryId = 1,
                Category = newCategory

            });

            Books.Add(new()
            {
                Id = 2,
                Title = "T-Shirt",
                Language="dansk",
                Description = "T-Shirt for boys",
                PublishYear = 1996,
                CategoryId = 1,
                Category = newCategory
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

    }
}
