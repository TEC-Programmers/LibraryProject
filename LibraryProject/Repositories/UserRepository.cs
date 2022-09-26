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
        Task<User> Delete(int userId);
        Task<User> DeleteWithProcedure(int userId);
        Task<User> UpdateRoleWithProcedure(int userId, User user);
        Task<User> UpdateProfileWithProcedure(int userId, User user);
        Task<User> UpdatePasswordWithProcedure(int userId, User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;
        

        public UserRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.FromSqlRaw("selectAllUsers").ToListAsync();
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


        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }


        public async Task<User> UpdatePasswordWithProcedure(int userId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", userId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
               new SqlParameter("@Password", BC.HashPassword(user.Password)), 
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserPassword @Id, @FirstName, @MiddleName, @LastName, @Email, @Password", parameters.ToArray());
            return user;
        }


        public async Task<User> UpdateRoleWithProcedure(int userId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", userId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
               new SqlParameter("@Role", user.Role)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserRole @Id, @FirstName, @MiddleName, @LastName, @Email, @Role", parameters.ToArray());
            return user;
        }


        public async Task<User> UpdateProfileWithProcedure(int userId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", userId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserProfile @Id, @FirstName, @MiddleName, @LastName, @Email", parameters.ToArray());
            return user;
        }
        
        public async Task<User> DeleteWithProcedure(int userId)
        {
          User deleteuser = await _context.Users
              .FirstOrDefaultAsync(u => u.Id == userId);

            if (deleteuser != null)
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC deleteUser @Id", userId);
                //_context.Users.Remove(deleteuser);
                //await _context.SaveChangesAsync();
            }
            return deleteuser;
        }

        public async Task<User> Delete(int userId)
        {
            User deleteuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (deleteuser != null)
            {
                _context.Users.Remove(deleteuser);
                await _context.SaveChangesAsync();
            }
            return deleteuser;
        }
    }
}