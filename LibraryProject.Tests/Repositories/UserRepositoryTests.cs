using Castle.Core.Configuration;
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
    public class UsersRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly UsersRepository _UsersRepository;
        private readonly Mock<API.Authorization.IJwtUtils> jwt = new();

        


        public UsersRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProject")
                .Options;
            _context = new(_options);  
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Users.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            });

            _context.Users.Add(new()

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
            var result = await _UsersRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Users>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _UsersRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Users>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectUsersById_ShouldReturnUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int UsersId = 1;


            _context.Users.Add(new()
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

            var result = await _UsersRepository.GetByIdWithProcedure(UsersId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Users>(result);
            Assert.Equal(UsersId, result.Id);
        }

        [Fact]
        public async void SelectUsersById_ShouldReturnNull_WhenUsersDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _UsersRepository.GetByIdWithProcedure(1);

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async void InsertNewUsers_ShouldAddNewIdToUsers_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;


            Users newUsers = new()
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

            var result = await _UsersRepository.registerWithProcedure(newUsers);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<Users>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewUsers_ShouldFailToAddNewUsers_WhenUsersIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Users Users = new()
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
            var result = await _UsersRepository.registerWithProcedure(Users);
          

            //Assert       
            Assert.NotNull(result);
            Assert.IsType<Users>(result);
            Assert.Equal(expectedNewId, result.Id);
     
        }


        [Fact]
        public async void UpdateExistingUsers_ShouldChangeValuesOnUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int UsersId = 1;

            Users newUsers = new()
            {

                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };

            _context.Users.Add(newUsers);
            await _context.SaveChangesAsync();

            Users updateUsers = new()
            {

                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };


            //Act 
            var result = await _UsersRepository.UpdateProfileWithProcedure(UsersId, updateUsers);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Users>(result);
            Assert.Equal(UsersId, result.Id);
            Assert.Equal(updateUsers.FirstName, result.FirstName);
            Assert.Equal(updateUsers.MiddleName, result.MiddleName);
            Assert.Equal(updateUsers.LastName, result.LastName);
            Assert.Equal(updateUsers.Email, result.Email);
            Assert.Equal(updateUsers.Password, result.Password);
        }

        [Fact]
        public async void UpdateExistingUsers_ShouldReturnNull_WhenUsersDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int UsersId = 1;

            Users updateUsers = new()
            {

                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };


            //Act
            var result = await _UsersRepository.UpdateProfileWithProcedure(UsersId, updateUsers);

            //Asert
            Assert.Null(result);

        }

        [Fact]
        public async void DeleteUsersById_ShouldReturnDeletedUsers_WhenUsersIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int UsersId = 1;

            Users newUsers = new()
            {

                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            };

            _context.Users.Add(newUsers);
            await _context.SaveChangesAsync();


            //Act
            var result = await _UsersRepository.DeleteWithProcedure(UsersId);
            var Users = await _UsersRepository.GetByIdWithProcedure(UsersId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Users>(result);
            Assert.Equal(UsersId, result.Id);
            Assert.Null(Users);
        }

        [Fact]
        public async void DeleteUsersById_ShouldReturnNull_WhenUsersDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _UsersRepository.DeleteWithProcedure(1);


            //Assert
            Assert.Null(result);

        }
    }
}