using System;
using Domain.Interfaces;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Domain.Repositories;
using Domain.Repositories.MoonClothHouse;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data.MoonClothHouse
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _dbContext;

        public ProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var productImages = await _dbContext.Products.ToListAsync();

            return productImages;
        }


        public async Task<Product> GetByIdAsync(string id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task<List<Product>> GetProductsByIdAsync(string id)
        {
            // Assuming Product has a property named "Id" for comparison
            return await _dbContext.Products.Where(p => p.ProductId == id).ToListAsync();
        }

        public async Task AddAsync(Product productImage)
        {
            _dbContext.Products.Add(productImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product productImage)
        {
            _dbContext.Entry(productImage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product productImage)
        {
            _dbContext.Products.Remove(productImage);
            await _dbContext.SaveChangesAsync();
        }

        Task<Product> IMoonClothHouseRepository<Product>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


