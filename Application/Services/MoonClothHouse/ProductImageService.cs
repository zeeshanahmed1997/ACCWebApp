using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Domain.Repositories;
using Domain.Repositories.MoonClothHouse;

namespace Application.Services.MoonClothHouse
{
    public class ProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImageAsync()
        {
            return await _productImageRepository.GetAllAsync();
        }

        public async Task<ProductImage> GetProductImageIdAsync(string id)
        {
            return await _productImageRepository.GetByIdAsync(id);
        }

        public async Task AddProductImageAsync(ProductImage productImage)
        {
            await _productImageRepository.AddAsync(productImage);
        }

        public async Task UpdateProductImageAsync(ProductImage productImage)
        {
            await _productImageRepository.UpdateAsync(productImage);
        }

        public async Task DeleteProductImageAsync(ProductImage productImage)
        {
            await _productImageRepository.DeleteAsync(productImage);
        }
    }
}


