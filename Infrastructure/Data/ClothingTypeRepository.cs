using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClothingTypeRepository : IClothingTypeRepository
    {
        private readonly DBContext _dbContext;

        public ClothingTypeRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClothingType>> GetAllAsync()
        {
            return await _dbContext.ClothingTypes.ToListAsync();
        }

        public async Task<ClothingType> GetByIdAsync(int id)
        {
            return await _dbContext.ClothingTypes.FindAsync(id);
        }

        public async Task AddAsync(ClothingType product)
        {
            _dbContext.ClothingTypes.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothingType product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothingType product)
        {
            _dbContext.ClothingTypes.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}


