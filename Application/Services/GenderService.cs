using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class GenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<IEnumerable<Gender>> GetGenderAsync()
        {
            return await _genderRepository.GetAllAsync();
        }

        public async Task<Gender> GetGenderByIdAsync(int id)
        {
            return await _genderRepository.GetByIdAsync(id);
        }

        public async Task AddGenderAsync(Gender gender)
        {
            await _genderRepository.AddAsync(gender);
        }

        public async Task UpdateGenderAsync(Gender gender)
        {
            await _genderRepository.UpdateAsync(gender);
        }

        public async Task DeleteGenderAsync(Gender gender)
        {
            await _genderRepository.DeleteAsync(gender);
        }
    }
}


