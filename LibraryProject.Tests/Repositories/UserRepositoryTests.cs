using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
using LibraryProject.API.Repositories;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly UserRepository _userRepository;
        private readonly Mock<API.Authorization.IJwtUtils> jwt = new();



        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProject")
                .Options;

            _context = new(_options);
            _userRepository = new(_context);

        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            });

            _context.User.Add(new()

            {
                Id = 2,
                FirstName = "Jack",
                MiddleName = "J.",
                LastName = "Hansen",
                Email = "jack@abc.com",
                Password = "password",
                Role = Role.Customer



            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;


            _context.User.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            });


            await _context.SaveChangesAsync();

            //Act

            var result = await _userRepository.GetById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.GetById(1);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void InsertNewUser_ShouldAddNewIdToUser_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;


            User newUser = new()
            {
                Id = expectedNewId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };



            //Act

            var result = await _userRepository.registerWithProcedure(newUser);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewUser_ShouldFailToAddNewUser_WhenUserIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            User user = new()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };
        
            await _context.SaveChangesAsync();

            //Act
            var result = await _userRepository.registerWithProcedure(user);
          

            //Assert       
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result.Id);
     
        }


        [Fact]
        public async void UpdateExistingUser_ShouldChangeValuesOnUser_WhenUserExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {

                Id = userId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            User updateUser = new()
            {

                Id = userId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };


            //Act
            var result = await _userRepository.Update(userId, updateUser);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateUser.FirstName, result.FirstName);
            Assert.Equal(updateUser.MiddleName, result.MiddleName);
            Assert.Equal(updateUser.LastName, result.LastName);
            Assert.Equal(updateUser.Email, result.Email);
            Assert.Equal(updateUser.Password, result.Password);
        }

        [Fact]
        public async void UpdateExistingUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User updateUser = new()
            {

                Id = userId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };


            //Act
            var result = await _userRepository.Update(userId, updateUser);

            //Asert
            Assert.Null(result);

        }

        [Fact]
        public async void DeleteUserById_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {

                Id = userId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();


            //Act
            var result = await _userRepository.Delete(userId);
            var user = await _userRepository.GetById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Null(user);
        }

        [Fact]
        public async void DeleteUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.Delete(1);


            //Assert
            Assert.Null(result);

        }
    }
}