using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class SalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            return await _salesRepository.GetAllAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _salesRepository.GetByIdAsync(id);
        }

        public async Task AddSaleAsync(Sale sales)
        {
            await _salesRepository.AddAsync(sales);
        }

        public async Task UpdateSaleAsync(Sale sales)
        {
            await _salesRepository.UpdateAsync(sales);
        }

        public async Task DeleteSaleAsync(Sale sales)
        {
            await _salesRepository.DeleteAsync(sales);
        }
    }
}


