using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
using LibraryProject.Database;
using LibraryProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    //Creating Interface of IUserRepository
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int userId);
        Task<User> Update(int userId, User user);
        Task<User> Delete(int userId);
    }

    public class UserRepository : IUserRepository            // This class is inheriting interfcae IUserRepository and implement the interfaces
    {
        private readonly LibraryProjectContext _context;         //making an instance of the class LibraryProjectContext

        public UserRepository(LibraryProjectContext context)         //dependency injection with parameter 
        {
            _context = context;
        }

        //**implementing the methods of IAuthorRepository interface**// 

        //This method will get all of the users information 
        public async Task<List<User>> GetAll()
        {

            return await _context.User.ToListAsync();

        }

        //With this method one user's info can be added
        public async Task<User> Create(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //This method will get one specific user info whoose userId has been given 
        public async Task<User> GetById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        //This method will get one specific user info whoose email has been given 
        public async Task<User> GetByEmail(string Email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == Email);
        }

        //Using this method existing user info can be updated by giving specific userId
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

        //This method will remove all the details of one user by userID
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