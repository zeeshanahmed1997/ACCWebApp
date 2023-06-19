using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Products> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(Products product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Products product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Products product)
        {
            await _productRepository.DeleteAsync(product);
        }
    }
}


