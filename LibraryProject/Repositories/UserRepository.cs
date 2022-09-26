using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAll();
        Task<Users> Create(Users user);
        Task<Users> GetByEmail(string email);
        Task<Users> GetById(int userId);
        Task<Users> Update(int userId, Users user);
        Task<Users> Delete(int userId);
        Task<Users> UpdateRole(int userId, Users user);

    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;

        public UserRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

       

        public async Task<Users> Create(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users> GetById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Users> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }


        public async Task<Users> UpdateRole(int userId, Users user)
        {
            Users updateUser = await _context.Users
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


        public async Task<Users> Update(int userId, Users user)
        {
            Users updateUser = await _context.Users
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

        public async Task<Users> Delete(int userId)
        {
            Users deleteuser = await _context.Users
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