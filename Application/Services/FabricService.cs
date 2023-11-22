using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class FabricService
    {
        private readonly IFabricRepository _fabricRepository;

        public FabricService(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        public async Task<IEnumerable<Fabric>> GetFabricAsync()
        {
            return await _fabricRepository.GetAllAsync();
        }

        public async Task<Fabric> GetFabricByIdAsync(int id)
        {
            return await _fabricRepository.GetByIdAsync(id);
        }

        public async Task AddFabricAsync(Fabric fabric)
        {
            await _fabricRepository.AddAsync(fabric);
        }

        public async Task UpdateFabricAsync(Fabric fabric)
        {
            await _fabricRepository.UpdateAsync(fabric);
        }

        public async Task DeleteFabricAsync(Fabric fabric)
        {
            await _fabricRepository.DeleteAsync(fabric);
        }
    }
}


