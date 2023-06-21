using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FabricRepository : IFabricRepository
    {
        private readonly DBContext _dbContext;

        public FabricRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Fabric>> GetAllAsync()
        {
            return await _dbContext.Fabrics.ToListAsync();
        }

        public async Task<Fabric> GetByIdAsync(int id)
        {
            return await _dbContext.Fabrics.FindAsync(id);
        }

        public async Task AddAsync(Fabric product)
        {
            _dbContext.Fabrics.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fabric product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fabric product)
        {
            _dbContext.Fabrics.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}


