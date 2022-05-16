using LibraryProject.API.Repositories;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly BookRepository _bookRepository;
        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectBooks")
                .Options;

            _context = new(_options);
            _bookRepository = new(_context);
        }
        [Fact]
        public async void SelectAllBooks_ShouldReturnListOfBooks_WhenBookExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            _context.Category.Add(new()
            {
                Id = 1,
                CategoryName = "Børnebog"

            });

       
            _context.Author.Add(new()
            {
                Id = 1,
                FirstName = "Astrid",
                MiddleName = "",
                LastName = " Lindgrens"

            });

            _context.Book.Add(new()
            {

                Id = 1,
                Title = "Pipi Langstrømper",
                Description = "Kids bog ",
                Language = "Dansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1
               


            });
            _context.Book.Add(new()
            {

                Id = 2,
                Title = "Karen begynder næsten i skole",
                Description = "Bøg for Børn",
                Language = "Dansk",
                PublishYear = 2022,
                CategoryId = 1,
                AuthorId= 1
               


            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _bookRepository.SelectAllBooks();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.Equal(2, result.Count);
            // Assert.Empty(result);
        }

    }
}
