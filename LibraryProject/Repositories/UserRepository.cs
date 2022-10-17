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
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    { 
        Task<List<User>> GetAllWithProcedure();
        Task<User> registerWithProcedure(User user);
        Task<User> register(User user);

        Task<User> GetByEmailWithProcedure(string email);
        Task<User> GetByIdWithProcedure(int UserId);
        Task<User> GetById(int UserId);
        Task<User> DeleteWithProcedure(int UserId);
        Task<User> Delete(int UserId);
        Task<User> UpdateRoleWithProcedure(int UserId, User user);
        Task<User> UpdateProfileWithProcedure(int UserId, User user);
        Task<User> UpdatePasswordWithProcedure(int UserId, User user);
        Task<User> Update(int UserId, User user);

    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;
        

        public UserRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllWithProcedure()
        {
            return await _context.Users.FromSqlRaw("selectAllUsers").ToListAsync();
        }

        public async Task<User> registerWithProcedure(User user)
        {
            var FirstName = new SqlParameter("@FirstName", user.FirstName);
            var MiddleName = new SqlParameter("@MiddleName", user.MiddleName);
            var LastName = new SqlParameter("@LastName", user.LastName);
            var Email = new SqlParameter("@Email", user.Email);
            var Password = new SqlParameter("@Password", user.Password);
            var Role = new SqlParameter("@Role", user.Role);           

            await _context.Database.ExecuteSqlRawAsync("exec insertUser " +
                "@FirstName, @MiddleName, @LastName, @Email, @Password, @Role",
                FirstName, MiddleName, LastName, Email, Password, Role);

            return user;
        }
        public async Task<User> register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> GetByIdWithProcedure(int UserId)
        {   
            User getUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == UserId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", UserId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC selectUserById @Id", parameter.ToArray());
            return getUser;
        }
        public async Task<User> GetById(int UserId)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == UserId);
        }
        public async Task<User> GetByEmailWithProcedure(string Email)
        {
            //return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);

            User getUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Email", Email)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC selectUserByEmail @Email", parameter.ToArray());
            return getUser;
        }
        public async Task<User> Update(int UserId, User user)
        {
            User upateUser = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == UserId);

            if (upateUser != null)
            {
                upateUser.FirstName = user.FirstName;
                upateUser.LastName = user.LastName;
                upateUser.MiddleName = user.MiddleName;
                upateUser.Email = user.Email;
                upateUser.Password = user.Password;
                await _context.SaveChangesAsync();
            }

            return upateUser;
        }

        public async Task<User> UpdatePasswordWithProcedure(int UserId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UserId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
               new SqlParameter("@Password", BC.HashPassword(user.Password)), 
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserPassword @Id, @FirstName, @MiddleName, @LastName, @Email, @Password", parameters.ToArray());
            return user;
        }
        public async Task<User> UpdateRoleWithProcedure(int UserId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UserId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
               new SqlParameter("@Role", user.Role)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserRole @Id, @FirstName, @MiddleName, @LastName, @Email, @Role", parameters.ToArray());
            return user;
        }
        public async Task<User> UpdateProfileWithProcedure(int UserId, User user)
        {
            var parameters = new List<SqlParameter>
            {
               new SqlParameter("@Id", UserId),
               new SqlParameter("@FirstName", user.FirstName),
               new SqlParameter("@MiddleName", user.MiddleName),
               new SqlParameter("@LastName", user.LastName),
               new SqlParameter("@Email", user.Email),
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC updateUserProfile @Id, @FirstName, @MiddleName, @LastName, @Email", parameters.ToArray());
            return user;
        }      
        public async Task<User> DeleteWithProcedure(int UserId)
        {
            User deleteuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == UserId);

            var parameter = new List<SqlParameter>
            {
               new SqlParameter("@Id", UserId)     
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC deleteUser @Id", parameter.ToArray());
            return deleteuser;
        }
        public async Task<User> Delete(int UserId)
        {     
            User deleteUser = await _context.Users
                .FirstOrDefaultAsync(User => User.Id == UserId);
            if (deleteUser != null)
            {
                _context.Users.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
            return deleteUser;
        }

    }
}