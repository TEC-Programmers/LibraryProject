using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using LibraryProject.Database;

namespace LibraryProject.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Create(User customer);
        // Task<User> GetByEmail(string email);
        // Task<User> GetById(int customerId);
        //  Task<User> Update(int customerId, User customer);
        // Task<User> Delete(int customerId);
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

        public async Task<User> Create(User customer)
        {


            _context.User.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

    }
}