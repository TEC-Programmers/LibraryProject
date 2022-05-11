using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using LibraryProject.API.Authorization;
using LibraryProject.API.DTO_s;
using  LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;

namespace LibraryProject.API.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int UserId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UserResponse> Register(RegisterUserRequest newUser);
        //  Task<UserResponse> Update(int UserId, RegisterUserRequest updateUser);
        // Task<UserResponse> Delete(int UserId);

    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }


        public async Task<List<UserResponse>> GetAll()
        {
            /**/
            List<User> users = await _userRepository.GetAll();

            return users == null ? null : users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Password = u.Password,             
                Role = u.Role
             }).ToList();

        }

        public async Task<UserResponse> Register(RegisterUserRequest newuser)
        {
            User user = new User
            {

                FirstName = newuser.FirstName,
                MiddleName = newuser.MiddleName,
                LastName = newuser.LastName,
                Email = newuser.Email,
                Password = newuser.Password,
                Role = Helpers.Role.Customer // force all users created through Register, to Role.User
            };

            user = await _userRepository.Create(user);

            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> GetById(int UserId)
        {
            User User = await _userRepository.GetById(UserId);

            if (User != null)
            {

                return MapUserToUserResponse(User);
            }
            return null;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            User user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,              
                    Password = user.Password,
                    Role = user.Role,
                  //  Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }


        private UserResponse MapUserToUserResponse(User user)
        {
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Password = user.Password,
                Role = user.Role             

            };
            
        }


    }
}