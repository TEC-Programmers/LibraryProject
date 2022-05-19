using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
