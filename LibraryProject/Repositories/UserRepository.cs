using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTO_s;
using LibraryProject.API.Helpers;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> registerWithProcedure(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int userId);
        Task<User> Update(int userId, User user);
        Task<User> Delete(int userId);
        Task<User> UpdateRole(int userId, User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;
        private readonly string _connectionString;

        public UserRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }


        public async Task<User> registerWithProcedure(User user)
        {
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertUser", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", user.MiddleName));
            cmd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
            cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
            cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@Role", user.Role));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return user;
        }


        //public async Task<User> Create(User user)
        //{
        //    _context.User.Add(user);
        //    await _context.SaveChangesAsync();
        //    return user;
        //}


        public async Task<User> GetById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == Email);
        }


        public async Task<User> UpdateRole(int userId, User user)
        {
            User updateUser = await _context.User
                .FirstOrDefaultAsync(a => a.Id == userId);


            if (updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.FirstName = user.FirstName;
                updateUser.MiddleName = user.MiddleName;
                updateUser.LastName = user.LastName;
                updateUser.Password = user.Password;
                updateUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }


        public async Task<User> Update(int userId, User user)
        {
            User updateUser = await _context.User
                .FirstOrDefaultAsync(a => a.Id == userId);
            

            if (updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.FirstName = user.FirstName;
                updateUser.MiddleName = user.MiddleName;
                updateUser.LastName = user.LastName;
                //updateUser.Password = user.Password;
                updateUser.Password = BC.HashPassword(user.Password);

                //updateUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }

        public async Task<User> Delete(int userId)
        {
            User deleteuser = await _context.User
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (deleteuser != null)
            {
                _context.User.Remove(deleteuser);
                await _context.SaveChangesAsync();
            }
            return deleteuser;
        }

    }
}