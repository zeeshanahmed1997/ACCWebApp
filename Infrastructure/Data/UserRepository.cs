using System;
using ACCWebApp.Models;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _dbContext;

        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task AddAsync(Users users)
        {
            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users users)
        {
            _dbContext.Entry(users).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Users users)
        {
            _dbContext.Users.Remove(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}


