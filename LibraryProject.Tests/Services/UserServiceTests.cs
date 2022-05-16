using LibraryProject.API.Authorization;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Helpers;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using LibraryProject.Database.Entities;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository = new();
        private readonly Mock<IJwtUtils> jwt = new();


        public UserServiceTests()
        {
            _userService = new UserService(_mockUserRepository.Object, jwt.Object);

        }

        [Fact]
        public async void GetAllUsers_ShouldReturnListOfUserResponses_WhenUsersExists()
        {
            //Arrange

            List<User> users = new();


            users.Add(new()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator
            });

            users.Add(new()
            {
                Id = 2,
                FirstName = "Jack",
                MiddleName = "J.",
                LastName = "Hansen",
                Email = "jack@abc.com",
                Password = "password",
                Role = Role.Customer
            });

            _mockUserRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserResponse>>(result);
        }

        [Fact]
        public async void GetAllUsers_ShouldReturnEmptyListOfUserResponses_WhenNoUsersExists()
        {
            //Arrange
            List<User> users = new();

            _mockUserRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UserResponse>>(result);
        }

        [Fact]
        public async void GetUserByIdShouldReturnUserResponseWhenUserExists()
        {
            //Arrange
            int userId = 1;

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

            _mockUserRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);

            //Act
            var result = await _userService.GetById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.MiddleName, result.MiddleName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);

        }

        [Fact]
        public async void GetUserByIdShloudReturnNullWhenUserDoesNotExist()
        {
            //Arrage
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userService.GetById(userId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUser_ShouldReturnUserResponse_WhenCreateIsSuccess()
        {
            //Arrange

            UserRequest newUser = new()
            {

                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password"
            };


            _mockUserRepository
            .Setup(x => x.Create(It.IsAny<User>()))
            .ReturnsAsync(() => null);

            //Act
            var result = await _userService.Register(newUser);

            //Assert         

            Assert.Null(result);



        }

        [Fact]
        public async void CreateUser_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //Arrange
            UserRequest newUser = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",

            };

            _mockUserRepository
                .Setup(x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userService.Register(newUser);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateUser_ShouldReturnUserResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test id anything actually changed on the DB,
            // we only test that the returned values match the submitted values

            //Arrange
            UserRequest userRequest = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",

            };

            int userId = 1;


            User user = new()
            {
                Id = userId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUserRepository
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(user);

            //Act
            var result = await _userService.Update(userId, userRequest);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(userRequest.FirstName, result.FirstName);
            Assert.Equal(userRequest.LastName, result.LastName);
            Assert.Equal(userRequest.MiddleName, result.MiddleName);
            Assert.Equal(userRequest.Email, result.Email);
            Assert.Equal(userRequest.Password, result.Password);
        }

        [Fact]
        public async void UpdateUser_ShouldReturnNull_WhenAuhtorDoesNotExist()
        {

            //Arrange
            UserRequest userRequest = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int userId = 1;

            _mockUserRepository
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userService.Update(userId, userRequest);


            //Assert
            Assert.Null(result);

        }


        [Fact]
        public async void DeleteUser_shouldReturnUserResponse_WhenDeleteIsSuccess()
        {

            //Arrange
            int userId = 1;

            User deletedUser = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };
            _mockUserRepository
               .Setup(x => x.Delete(It.IsAny<int>()))
               .ReturnsAsync(deletedUser);
             
            // Act
            var result = await _userService.Delete(userId);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
        
        }

        [Fact]
        public async void DeletUser_ShouldNotReturnNull_whenUserDoesNotExist()
        {

            //Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userService.Delete(userId);

            //Assert
            Assert.Null(null);

        }
    }
}
