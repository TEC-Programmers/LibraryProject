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
    public interface IUsersRepository
    { 
        Task<List<Users>> GetAll();
        Task<Users> registerWithProcedure(Users Users);
        Task<Users> GetByEmail(string email);
        Task<Users> GetByIdWithProcedure(int UsersId);
        Task<Users> DeleteWithProcedure(int UsersId);
        Task<Users> UpdateRoleWithProcedure(int UsersId, Users Users);
        Task<Users> UpdateProfileWithProcedure(int UsersId, Users Users);
        Task<Users> UpdatePasswordWithProcedure(int UsersId, Users Users);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryProjectContext _context;
        public string _connectionString;
        

        public UsersRepository(LibraryProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.FromSqlRaw("selectAllUserss").ToListAsync();
        }


        public async Task<Users> registerWithProcedure(Users Users)
        {
            using SqlConnection sql = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("insertUsers", sql);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FirstName", Users.FirstName));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", Users.MiddleName));
            cmd.Parameters.Add(new SqlParameter("@LastName", Users.LastName));
            cmd.Parameters.Add(new SqlParameter("@Email", Users.Email));
            cmd.Parameters.Add(new SqlParameter("@Password", Users.Password));
            cmd.Parameters.Add(new SqlParameter("@Role", Users.Role));

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return Users;
        }


        public async Task<Users> GetByIdWithProcedure(int userId)
        {
            //return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            Users getUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", userId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC selectUserById @Id", parameter.ToArray());
            return getUser;
        }

        public async Task<Users> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }


        public async Task<Users> UpdatePasswordWithProcedure(int UsersId, Users Users)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UsersId),
               new SqlParameter("@FirstName", Users.FirstName),
               new SqlParameter("@MiddleName", Users.MiddleName),
               new SqlParameter("@LastName", Users.LastName),
               new SqlParameter("@Email", Users.Email),
               new SqlParameter("@Password", BC.HashPassword(Users.Password)), 
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUsersPassword @Id, @FirstName, @MiddleName, @LastName, @Email, @Password", parameters.ToArray());
            return Users;
        }


        public async Task<Users> UpdateRoleWithProcedure(int UsersId, Users Users)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UsersId),
               new SqlParameter("@FirstName", Users.FirstName),
               new SqlParameter("@MiddleName", Users.MiddleName),
               new SqlParameter("@LastName", Users.LastName),
               new SqlParameter("@Email", Users.Email),
               new SqlParameter("@Role", Users.Role)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUsersRole @Id, @FirstName, @MiddleName, @LastName, @Email, @Role", parameters.ToArray());
            return Users;
        }


        public async Task<Users> UpdateProfileWithProcedure(int UsersId, Users Users)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UsersId),
               new SqlParameter("@FirstName", Users.FirstName),
               new SqlParameter("@MiddleName", Users.MiddleName),
               new SqlParameter("@LastName", Users.LastName),
               new SqlParameter("@Email", Users.Email),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUsersProfile @Id, @FirstName, @MiddleName, @LastName, @Email", parameters.ToArray());
            return Users;
        }
        
        public async Task<Users> DeleteWithProcedure(int UsersId)
        {
            Users deleteUsers = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == UsersId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", UsersId)     
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteUsers @Id", parameter.ToArray());
            return deleteUsers;
        }
  
    }
}