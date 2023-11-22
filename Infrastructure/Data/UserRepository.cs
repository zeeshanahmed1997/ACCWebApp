using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task AddAsync(User users)
        {
            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User users)
        {
            _dbContext.Entry(users).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User users)
        {
            _dbContext.Users.Remove(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}


