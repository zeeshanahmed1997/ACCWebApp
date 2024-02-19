using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly DBContext _dbContext;

        public ClothingRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Clothing>> GetAllAsync()
        {
            return await _dbContext.Clothings.ToListAsync();
        }

        public async Task<Clothing> GetByIdAsync(int id)
        {
            return await _dbContext.Clothings.FindAsync(id);
        }

        public async Task AddAsync(Clothing product)
        {
            _dbContext.Clothings.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Clothing product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Clothing product)
        {
            _dbContext.Clothings.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}


