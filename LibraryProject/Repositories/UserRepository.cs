using LibraryProject.API.Helpers;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
       // Task<List<User>> GetAdmins();
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int userId);
        Task<User> Update(int userId, User user);
        Task<User> Delete(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly LibraryProjectContext _context;

        public UserRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {

            return await _context.User.ToListAsync();

        }

       

        public async Task<User> Create(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == Email);
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
                updateUser.Password = user.Password;
                updateUser.Role = user.Role;
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