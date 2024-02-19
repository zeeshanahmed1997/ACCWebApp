using System;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class ClothingTypeService
	{

         private readonly IClothingTypeRepository _clothingTypeRepository;

        public ClothingTypeService(IClothingTypeRepository clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<IEnumerable<ClothingType>> GetClothingTypeAsync()
        {
            return await _clothingTypeRepository.GetAllAsync();
        }

        public async Task<ClothingType> GetClothingTypeByIdAsync(int id)
        {
            return await _clothingTypeRepository.GetByIdAsync(id);
        }

        public async Task AddClothingTypeAsync(ClothingType clothing)
        {
            await _clothingTypeRepository.AddAsync(clothing);
        }

        public async Task UpdateCothingTypeAsync(ClothingType clothing)
        {
            await _clothingTypeRepository.UpdateAsync(clothing);
        }

        public async Task DeleteClothingTypeAsync(ClothingType clothing)
        {
            await _clothingTypeRepository.DeleteAsync(clothing);
        }
    }
	
}

