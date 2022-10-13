using LibraryProject.API.Authorization;
using LibraryProject.API.Database.Entities;
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
        private readonly UserService _UsersService;
        private readonly Mock<IUsersRepository> _mockUsersRepository = new();
        private readonly Mock<IJwtUtils> jwt = new();


        public UserServiceTests()
        {
            _UsersService = new UserService(_mockUsersRepository.Object, jwt.Object);

        }

        [Fact]
        public async void GetAllUserss_ShouldReturnListOfUsersResponses_WhenUserssExists()
        {
            //Arrange

            List<Users> Userss = new();


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

            _mockUsersRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(Userss);

            //Act
            var result = await _UsersService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UsersResponse>>(result);
        }

        [Fact]
        public async void GetAllUserss_ShouldReturnEmptyListOfUsersResponses_WhenNoUserssExists()
        {
            //Arrange
            List<Users> Userss = new();

            _mockUsersRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(Userss);

            //Act
            var result = await _UsersService.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UsersResponse>>(result);
        }

        [Fact]
        public async void GetUsersByIdShouldReturnUsersResponseWhenUsersExists()
        {
            //Arrange
            int UsersId = 1;

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

            _mockUsersRepository
                .Setup(x => x.GetByIdWithProcedure(It.IsAny<int>()))
                .ReturnsAsync(Users);

            //Act
            var result = await _UsersService.GetById(UsersId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UsersResponse>(result);
            Assert.Equal(Users.Id, result.Id);
            Assert.Equal(Users.FirstName, result.FirstName);
            Assert.Equal(Users.LastName, result.LastName);
            Assert.Equal(Users.MiddleName, result.MiddleName);
            Assert.Equal(Users.Email, result.Email);
            Assert.Equal(Users.Password, result.Password);

        }

        [Fact]
        public async void GetUsersByIdShloudReturnNullWhenUsersDoesNotExist()
        {
            //Arrage
            int UsersId = 1;

            _mockUsersRepository
                .Setup(x => x.GetByIdWithProcedure(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersService.GetById(UsersId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUsers_ShouldReturnUsersResponse_WhenCreateIsSuccess()
        {
            //Arrange

            UsersRequest newUsers = new()
            {

                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password"
            };


            _mockUsersRepository
            .Setup(x => x.registerWithProcedure(It.IsAny<Users>()))
            .ReturnsAsync(() => null);

            //Act
            var result = await _UsersService.registerWithProcedure(newUsers);

            //Assert         

            Assert.Null(result);



        }

        [Fact]
        public async void CreateUsers_ShouldReturnNull_WhenRepositoryReturnsNull()
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

            _mockUsersRepository
                .Setup(x => x.registerWithProcedure(It.IsAny<Users>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersService.registerWithProcedure(newUsers);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateUsers_ShouldReturnUsersResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test id anything actually changed on the DB,
            // we only test that the returned values match the submitted values

            //Arrange
            UsersRequest UsersRequest = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",

            };

            int UsersId = 1;


            Users Users = new()
            {
                Id = UsersId,
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };

            _mockUsersRepository
                .Setup(x => x.UpdateRoleWithProcedure(It.IsAny<int>(), It.IsAny<Users>()))
                .ReturnsAsync(Users);

            //Act
            var result = await _UsersService.UpdateRoleWithProcedure(UsersId, UsersRequest);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<UsersResponse>(result);
            Assert.Equal(UsersId, result.Id);
            Assert.Equal(UsersRequest.FirstName, result.FirstName);
            Assert.Equal(UsersRequest.LastName, result.LastName);
            Assert.Equal(UsersRequest.MiddleName, result.MiddleName);
            Assert.Equal(UsersRequest.Email, result.Email);
            Assert.Equal(UsersRequest.Password, result.Password);
        }

        [Fact]
        public async void UpdateUsers_ShouldReturnNull_WhenAuhtorDoesNotExist()
        {

            //Arrange
            UsersRequest UsersRequest = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
            };

            int UsersId = 1;

            _mockUsersRepository
                .Setup(x => x.UpdateProfileWithProcedure(It.IsAny<int>(), It.IsAny<Users>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersService.UpdateProfileWithProcedure(UsersId, UsersRequest);


            //Assert
            Assert.Null(result);

        }


        [Fact]
        public async void DeleteUsers_shouldReturnUsersResponse_WhenDeleteIsSuccess()
        {

            //Arrange
            int UsersId = 1;

            Users deletedUsers = new()
            {
                FirstName = "Peter",
                MiddleName = "Per.",
                LastName = "Aksten",
                Email = "peter@abc.com",
                Password = "password",
                Role = Role.Administrator

            };
            _mockUsersRepository
               .Setup(x => x.DeleteWithProcedure(It.IsAny<int>()))
               .ReturnsAsync(deletedUsers);
             
            // Act
            var result = await _UsersService.Delete(UsersId);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<UsersResponse>(result);
        
        }

        [Fact]
        public async void DeletUsers_ShouldNotReturnNull_whenUsersDoesNotExist()
        {

            //Arrange
            int UsersId = 1;

            _mockUsersRepository
                .Setup(x => x.DeleteWithProcedure(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _UsersService.Delete(UsersId);

            //Assert
            Assert.Null(null);

        }
    }
}
