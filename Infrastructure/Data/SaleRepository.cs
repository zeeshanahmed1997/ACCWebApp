using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SaleRepository : ISalesRepository
    {
        private readonly DBContext _dbContext;

        public SaleRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _dbContext.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _dbContext.Sales.FindAsync(id);
        }

        public async Task AddAsync(Sale product)
        {
            _dbContext.Sales.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sale product)
        {
            _dbContext.Sales.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}


