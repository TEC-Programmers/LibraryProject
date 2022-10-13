using System;
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
    public class UsersControllerTests
    {
        private readonly UsersController _UsersController;
        private readonly Mock<IUserService> _mockUsersService = new();

        public UsersControllerTests()
        {
            _UsersController = new(_mockUsersService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenUsersExists()
        {
            //Arrange
            List<UsersResponse> Userss = new();
            Userss.Add(new()
            {

                Id = 1,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            });

            Userss.Add(new()
            {
                Id = 2,
                FirstName = "Jack",
                MiddleName = "J.",
                LastName = "Hansen",
                Email = "jack@abc.com",
                Password = "password",
                Role = Role.Customer

            });

            _mockUsersService
                .Setup(x => x.GetAll())
                .ReturnsAsync(Userss);

            //Act
            var result = await _UsersController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoUsersExists()
        {
            //Arrange

            List<UsersResponse> Userss = new();

            _mockUsersService
                .Setup(x => x.GetAll())
                .ReturnsAsync(Userss);

            //Act
            var result = await _UsersController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange                      
            _mockUsersService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange                      
            _mockUsersService
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _UsersController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange
            int UsersId = 1;
            UsersResponse Users = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUsersService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(Users);

            //Act
            var result = await _UsersController.GetById(UsersId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUsersDoesNotExists()
        {
            //Arrange
            int UsersId = 1;

            _mockUsersService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersController.GetById(UsersId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange

            _mockUsersService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _UsersController.GetById(1);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenUsersIsSuccessfullyCreated()
        {
            //Arrange
            UsersRequest newUsers = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",


            };

            int UsersId = 1;

            UsersResponse UsersResponse = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUsersService
                .Setup(x => x.registerWithProcedure(It.IsAny<UsersRequest>()))
                .ReturnsAsync(UsersResponse);

            //Act
            var result = await _UsersController.RegisterWithProcedure(newUsers);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            UsersRequest newUsers = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",

            };

            _mockUsersService
                .Setup(x => x.registerWithProcedure(It.IsAny<UsersRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _UsersController.RegisterWithProcedure(newUsers);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUsersIsSuccessfullyUpdate()
        {
            //Arrange
            UsersRequest updateUsers = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int UsersId = 1;

            UsersResponse UsersResponse = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUsersService
                .Setup(x => x.UpdateRoleWithProcedure(It.IsAny<int>(), It.IsAny<UsersRequest>()))
                .ReturnsAsync(UsersResponse);

            //Act
            var result = await _UsersController.Update(UsersId, updateUsers);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            UsersRequest updateUsers = new UsersRequest
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int UsersId = 1;


            _mockUsersService
                .Setup(x => x.UpdateProfileWithProcedure(It.IsAny<int>(), It.IsAny<UsersRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            //Act
            var result = await _UsersController.Update(UsersId, updateUsers);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenUsersIsDeleted()
        {
            //Arrange
            int UsersId = 1;

            UsersResponse UsersResponse = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUsersService
               .Setup(x => x.Delete(It.IsAny<int>()))
               .ReturnsAsync(UsersResponse);

            //Act
            var result = await _UsersController.Delete(UsersId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTryingToDeleteUsersWhichDoesNotExist()
        {
            //Arrange
            int UsersId = 1;

            _mockUsersService
                 .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersController.Delete(UsersId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange
            int UsersId = 1;

            _mockUsersService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            //Act
            var result = await _UsersController.Delete(UsersId);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        /***/

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenLoginIsSuccessfullyCreated()
        {
            //Arrange
            LoginRequest newLogin = new()
            {
                Email = "peter@abc.com",
                Password = "password"

            };

            int UsersId = 1;

            LoginResponse loginResponse = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator,
                Token = ""
            };

            _mockUsersService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(loginResponse);

            //Act
            var result = await _UsersController.Authenticate(newLogin);

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
            _mockUsersService
                .Setup(x => x.Authenticate(It.IsAny<LoginRequest>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //Act
            var result = await _UsersController.Authenticate(newLogin);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }
    }
}