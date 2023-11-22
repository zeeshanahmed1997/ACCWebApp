using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class ClothingService
    {
        private readonly IClothingRepository _clothingRepository;

        public ClothingService(IClothingRepository clothingRepository)
        {
            _clothingRepository = clothingRepository;
        }

        public async Task<IEnumerable<Clothing>> GetClothesAsync()
        {
            return await _clothingRepository.GetAllAsync();
        }

        public async Task<Clothing> GetClothByIdAsync(int id)
        {
            return await _clothingRepository.GetByIdAsync(id);
        }

        public async Task AddClothAsync(Clothing clothing)
        {
            await _clothingRepository.AddAsync(clothing);
        }

        public async Task UpdateProductAsync(Clothing clothing)
        {
            await _clothingRepository.UpdateAsync(clothing);
        }

        public async Task DeleteProductAsync(Clothing clothing)
        {
            await _clothingRepository.DeleteAsync(clothing);
        }
    }
}


