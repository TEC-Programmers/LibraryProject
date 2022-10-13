using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryProject.API.Authorization;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Helpers;
using LibraryProject.API.Repositories;
using LibraryProject.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using BC = BCrypt.Net.BCrypt;

namespace LibraryProject.API.Services
{
    public interface IUserService
    {
        Task<List<UsersResponse>> GetAll();
        Task<UsersResponse> GetById(int UsersId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UsersResponse> registerWithProcedure(UsersRequest newUsers);
        Task<UsersResponse> UpdateProfileWithProcedure(int UsersId, UsersRequest updateUsers);
        Task<UsersResponse> Delete(int UsersId);
        Task<UsersResponse> UpdateRoleWithProcedure(int UsersId, UsersRequest updateUsers);
        Task<UsersResponse> UpdatePasswordWithProcedure(int UsersId, UsersRequest updateUsers);

    }

    public class UserService : IUserService
    {
        private readonly IUsersRepository _UsersRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUsersRepository UsersRepository, IJwtUtils jwtUtils)
        {
            _UsersRepository = UsersRepository;
            _jwtUtils = jwtUtils;

        }


        public async Task<List<UsersResponse>> GetAll()
        {

            List<Users> Users = await _UsersRepository.GetAll();
          

            return Users == null ? null : Users.Select(u => new UsersResponse
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


        public async Task<UsersResponse> registerWithProcedure(UsersRequest newUsers)
        {

            Users Users = new Users
            {
                FirstName = newUsers.FirstName,
                MiddleName = newUsers.MiddleName,
                LastName = newUsers.LastName,
                Email = newUsers.Email,
                Password = BC.HashPassword(newUsers.Password),
                Role = Helpers.Role.Customer // force all Users created through Register, to Role.Users
            };

            Users = await _UsersRepository.registerWithProcedure(Users);

            return MapUsersToUsersResponse(Users);
        }

        public async Task<UsersResponse> GetById(int UsersId)
        {
            Users Users = await _UsersRepository.GetByIdWithProcedure(UsersId);

            if (Users != null)
            {

                return MapUsersToUsersResponse(Users);
            }
            return null;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            Users Users = await _UsersRepository.GetByEmail(login.Email);

            if (Users == null)
            {
                return null;
            }
            //BC.Verify(login.Password, Users.Password)
            //Users.Password == login.Password
            if (BC.Verify(login.Password, Users.Password))
            {
                LoginResponse response = new LoginResponse
                {
                    Id = Users.Id,
                    Email = Users.Email,
                    FirstName = Users.FirstName,
                    MiddleName = Users.MiddleName,
                    LastName = Users.LastName,
                    Password = Users.Password,
                    Role = Users.Role,
                    Token = _jwtUtils.GenerateJwtToken(Users)
                };
                return response;
            }

            return null;
        }


        //UpdatePasswordWithProcedure
        public async Task<UsersResponse> UpdatePasswordWithProcedure(int UsersId, UsersRequest updateUsers)
        {
            Users Users = new()
            {
                FirstName = updateUsers.FirstName,
                MiddleName = updateUsers.MiddleName,
                LastName = updateUsers.LastName,
                Email = updateUsers.Email,
                Password = updateUsers.Password,
            };

            Users = await _UsersRepository.UpdatePasswordWithProcedure(UsersId, Users);

            return Users == null ? null : new UsersResponse
            {
                Id = Users.Id,
                FirstName = Users.FirstName,
                MiddleName = Users.MiddleName,
                LastName = Users.LastName,
                Email = Users.Email,
                Password = Users.Password,
                Role = Users.Role
            };
        }

        public async Task<UsersResponse> UpdateRoleWithProcedure(int UsersId, UsersRequest updateUsers)
        {
            Users Users = new()
            {
                FirstName = updateUsers.FirstName,
                MiddleName = updateUsers.MiddleName,
                LastName = updateUsers.LastName,
                Email = updateUsers.Email,
                Password = updateUsers.Password,
            };

            Users = await _UsersRepository.UpdateRoleWithProcedure(UsersId, Users);

            return Users == null ? null : new UsersResponse
            {
                Id = Users.Id,
                FirstName = Users.FirstName,
                MiddleName = Users.MiddleName,
                LastName = Users.LastName,
                Email = Users.Email,
                Password = Users.Password,
                Role = Users.Role
            };
        }


        public async Task<UsersResponse> UpdateProfileWithProcedure(int UsersId, UsersRequest updateUsers)
        {
            Users Users = new()
            {
                FirstName = updateUsers.FirstName,
                MiddleName = updateUsers.MiddleName,
                LastName = updateUsers.LastName,
                Email = updateUsers.Email,
                Password = updateUsers.Password,
            };

            Users = await _UsersRepository.UpdateProfileWithProcedure(UsersId, Users);

            return Users == null ? null : new UsersResponse
            {
                Id = Users.Id,
                FirstName = Users.FirstName,
                MiddleName = Users.MiddleName,
                LastName = Users.LastName,
                Email = Users.Email,
                Password = Users.Password,
                Role = Users.Role
            };
        }


        public async Task<UsersResponse> Delete(int UsersId)

        {
            Users Users = await _UsersRepository.DeleteWithProcedure(UsersId);

            if (Users != null)
            {
                return MapUsersToUsersResponse(Users);
            }

            return null;
        }


        private static UsersResponse MapUsersToUsersResponse(Users Users)
        {
           
            return Users == null ? null : new UsersResponse
            {
                Id = Users.Id,
                Email = Users.Email,
                FirstName = Users.FirstName,
                MiddleName = Users.MiddleName,
                LastName = Users.LastName,
                Password = Users.Password,
                Role = Users.Role
            };

        }
    }
}