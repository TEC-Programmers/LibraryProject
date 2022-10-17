using System.Collections.Generic;
using LibraryProject.API.Controllers;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Helpers;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;

namespace LibraryProject.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockuserService = new();

        public UserControllerTests()
        {
            _userController = new(_mockuserService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenuserExists()
        {
            //Arrange
            List<UserResponse> users = new();
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

            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNouserExists()
        {
            //Arrange

            List<UserResponse> users = new();

            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange                      
            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange                      
            _mockuserService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int UserId = 1;
            UserResponse User = new()
            {
                Id = UserId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockuserService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(User);

            //Act
            var result = await _userController.GetById(UserId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUserDoesNotExists()
        {
            //Arrange
            int UserId = 1;

            _mockuserService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.GetById(UserId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockuserService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _userController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenUserIsSuccessfullyCreated()
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

            int UserId = 1;

            UserResponse userResponse = new()
            {
                Id = UserId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockuserService
                .Setup(x => x.registerWithProcedure(It.IsAny<UserRequest>()))
                .ReturnsAsync(userResponse);

            //Act
            var result = await _userController.RegisterWithProcedure(newUser);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
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

            _mockuserService
                .Setup(x => x.registerWithProcedure(It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _userController.RegisterWithProcedure(newUser);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUserIsSuccessfullyUpdate()
        {
            //Arrange
            UserRequest updateUser = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int UserId = 1;

            UserResponse userResponse = new()
            {
                Id = UserId,
                FirstName = "Peter updated",
                MiddleName = "Per updated",
                LastName = "Aksten updated",
                Email = "peter@abc.com",
                Password = "password",              
            };

            _mockuserService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(userResponse);

            //Act
            var result = await _userController.Update(UserId, updateUser);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            UserRequest updateUser = new UserRequest
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int UserId = 1;


            _mockuserService
                .Setup(x => x.UpdateProfileWithProcedure(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _userController.Update(UserId, updateUser);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenUserIsDeleted()
        {
            //Arrange
            int UserId = 1;

            UserResponse userResponse = new()
            {
                Id = UserId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockuserService
               .Setup(x => x.Delete(It.IsAny<int>()))
               .ReturnsAsync(userResponse);

            //Act
            var result = await _userController.Delete(UserId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteUserWhichDoesNotExist()
        {
            //Arrange
            int UserId = 1;

            _mockuserService
                 .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.Delete(UserId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int UserId = 1;

            _mockuserService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            //Act
            var result = await _userController.Delete(UserId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenLoginIsSuccessfullyCreated()
        {
            //Arrange
            LoginRequest newLogin = new()
            {
                Email = "peter@abc.com",
                Password = "password"

            };

            int UserId = 1;

            LoginResponse loginResponse = new()
            {
                Id = UserId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator,
                Token = ""
            };

            _mockuserService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(loginResponse);

            //Act
            var result = await _userController.Authenticate(newLogin);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            //Arrange
            LoginRequest newLogin = new()
            {
                Email = "peter@abc.com",
                Password = "password"

            };
            _mockuserService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _userController.Authenticate(newLogin);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
    }
}