﻿using System;
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
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int UserId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UserResponse> registerWithProcedure(UserRequest newUser);
        Task<UserResponse> UpdateProfileWithProcedure(int UserId, UserRequest updateUser);
        Task<UserResponse> Delete(int UserId);
        Task<UserResponse> UpdateRoleWithProcedure(int UserId, UserRequest updateUser);
        Task<UserResponse> UpdatePasswordWithProcedure(int UserId, UserRequest updateUser);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;

        }


        public async Task<List<UserResponse>> GetAll()
        {

            List<User> users = await _userRepository.GetAllWithProcedure();
          

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

        public async Task<UserResponse> registerWithProcedure(UserRequest newuser)
        {

            User user = new User
            {
                FirstName = newuser.FirstName,
                MiddleName = newuser.MiddleName,
                LastName = newuser.LastName,
                Email = newuser.Email,
                Password = BC.HashPassword(newuser.Password),
                Role = Helpers.Role.Customer // force all users created through Register, to Role.Customer
            };

            user = await _userRepository.registerWithProcedure(user);

            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> GetById(int UserId)
        {
            User User = await _userRepository.GetByIdWithProcedure(UserId);

            if (User != null)
            {

                return MapUserToUserResponse(User);
            }
            return null;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            User user = await _userRepository.GetByEmailWithProcedure(login.Email);

            if (user == null)
            {
                return null;
            }
            //BC.Verify(login.Password, user.Password)
            //user.Password == login.Password
            if (BC.Verify(login.Password, user.Password))
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
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

        public async Task<UserResponse> UpdatePasswordWithProcedure(int UserId, UserRequest updateUser)
        {
            User user = new()
            {
                FirstName = updateUser.FirstName,
                MiddleName = updateUser.MiddleName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Password = updateUser.Password,
            };

            user = await _userRepository.UpdatePasswordWithProcedure(UserId, user);

            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }

        public async Task<UserResponse> UpdateRoleWithProcedure(int UserId, UserRequest updateUser)
        {
            User user = new()
            {
                FirstName = updateUser.FirstName,
                MiddleName = updateUser.MiddleName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Password = updateUser.Password,
            };

            user = await _userRepository.UpdateRoleWithProcedure(UserId, user);

            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }

        public async Task<UserResponse> UpdateProfileWithProcedure(int UserId, UserRequest updateUser)
        {
            User user = new()
            {
                FirstName = updateUser.FirstName,
                MiddleName = updateUser.MiddleName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Password = updateUser.Password,
            };

            user = await _userRepository.UpdateProfileWithProcedure(UserId, user);

            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }

        public async Task<UserResponse> Delete(int userId)

        {
            User user = await _userRepository.DeleteWithProcedure(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        private static UserResponse MapUserToUserResponse(User user)
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