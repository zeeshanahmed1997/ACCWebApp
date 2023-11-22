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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly DBContext _dbContext;

        public ProductImageRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            var productImages = await _dbContext.ProductImages.ToListAsync();

            return productImages;
        }


        public async Task<ProductImage> GetByIdAsync(string id)
        {
            return await _dbContext.ProductImages.FindAsync(id);
        }

        public async Task AddAsync(ProductImage productImage)
        {
            _dbContext.ProductImages.Add(productImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductImage productImage)
        {
            _dbContext.Entry(productImage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductImage productImage)
        {
            _dbContext.ProductImages.Remove(productImage);
            await _dbContext.SaveChangesAsync();
        }

        Task<ProductImage> IMoonClothHouseRepository<ProductImage>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}


