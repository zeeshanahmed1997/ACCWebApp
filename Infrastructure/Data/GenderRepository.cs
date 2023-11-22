using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DBContext _dbContext;

        public GenderRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _dbContext.Genders.ToListAsync();
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            return await _dbContext.Genders.FindAsync(id);
        }

        public async Task AddAsync(Gender gender)
        {
            _dbContext.Genders.Add(gender);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gender gender)
        {
            _dbContext.Entry(gender).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Gender gender)
        {
            _dbContext.Genders.Remove(gender);
            await _dbContext.SaveChangesAsync();
        }
    }
}


