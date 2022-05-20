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
        }

        [Fact]
        public async void SelectAllBooks_ShouldReturnEmptyListOfBooks_WhenNoBookExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _bookRepository.SelectAllBooks();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);

            Assert.Empty(result);
        }

        [Fact]
        public async void SelectBookById_ShouldReturnBook_WhenBookExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int bookId = 1;

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
                Id = bookId,
                Title = "Pipi Langstrømper",
                Description = "Kids bog ",
                Language = "Dansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1

            });


            await _context.SaveChangesAsync();

            //Act
            var result = await _bookRepository.SelectBookById(bookId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public async void SelectBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();




            //Act
            var result = await _bookRepository.SelectBookById(1);

            //Assert


            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewBook_ShouldAddnewIdToBook_WhenSavingToDatabase()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Book book = new()
            {
                Id = 1,
                Title = "Pipi Langstrømper",
                Description = "Kids bog ",
                Language = "Dansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1



            };


            await _context.SaveChangesAsync();

            //Act
            var result = await _bookRepository.InsertNewBook(book);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(expectedNewId, result.Id);

        }

        [Fact]
        public async void InsertNewProduct_ShouldFailToAddNewProduct_WhenProductIdAlreadyExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();



            Book book = new()
            {
                Id = 1,
                Title = "Pipi Langstrømper",
                Description = "Kids bog ",
                Language = "Dansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _bookRepository.InsertNewBook(book);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);

        }

        [Fact]
        public async void UpdateExistingBook_ShouldChangeValuesOnBook_WhenBookExists()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int bookId = 1;
          
            Book newBook = new()
            {
                Id = 1,
                Title = "Pipi Langstrømper",
                Description = "Kids bog ",
                Language = "Dansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1


            };

            _context.Book.Add(newBook);
            await _context.SaveChangesAsync();

            Book updateBook = new()
            {
                Id = bookId,
                Title = "Updated Pipi Langstrømper",
                Description = " updated Kids bog ",
                Language = " updatedDansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1

            };



            //Act
            var result = await _bookRepository.UpdateExistingBook(bookId, updateBook);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(updateBook.Title, result.Title);
            Assert.Equal(updateBook.Language, result.Language);
            Assert.Equal(updateBook.Description, result.Description);
            Assert.Equal(updateBook.PublishYear, result.PublishYear);
            Assert.Equal(updateBook.CategoryId, result.CategoryId);
            Assert.Equal(updateBook.AuthorId, result.AuthorId);


        }

        [Fact]
        public async void UpdateExistingBook_ShouldReturnNull_WhenBookDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int bookId = 1;


            Book updateBook = new()
            {
                Id = bookId,
                Title = "Updated Pipi Langstrømper",
                Description = " updated Kids bog ",
                Language = " updatedDansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1

            };



            //Act
            var result = await _bookRepository.UpdateExistingBook(bookId, updateBook);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void DeleteBookById_ShouldReturnDeleteBook_WhenBookIsDeleted()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();
            int bookId = 1;
            Book newBook = new()
            {
                Id = bookId,
                Title = "Updated Pipi Langstrømper",
                Description = " updated Kids bog ",
                Language = " updatedDansk",
                PublishYear = 1945,
                CategoryId = 1,
                AuthorId = 1

            };

            _context.Book.Add(newBook);
            await _context.SaveChangesAsync();




            //Act
            var result = await _bookRepository.DeleteBookById(bookId);
            var book = await _bookRepository.SelectBookById(bookId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.Id);
            Assert.Null(book);
        }

        [Fact]
        public async void DeleteBookById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            //Arrange 
            await _context.Database.EnsureDeletedAsync();


            //Act
            var result = await _bookRepository.DeleteBookById(1);


            //Assert
            Assert.Null(result);
        }
    }
}
