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

        public async Task<IEnumerable<Sales>> GetSalesAsync()
        {
            return await _salesRepository.GetAllAsync();
        }

        public async Task<Sales> GetSaleByIdAsync(int id)
        {
            return await _salesRepository.GetByIdAsync(id);
        }

        public async Task AddSaleAsync(Sales sales)
        {
            await _salesRepository.AddAsync(sales);
        }

        public async Task UpdateSaleAsync(Sales sales)
        {
            await _salesRepository.UpdateAsync(sales);
        }

        public async Task DeleteSaleAsync(Sales sales)
        {
            await _salesRepository.DeleteAsync(sales);
        }

        //public async Task GetSaleByIdsAsync(IEnumerable<int> ids)
        //{
        //    await _salesRepository.GetByIdAsync(ids);
        //}
    }
}


