using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Domain.Repositories;
using Domain.Repositories.MoonClothHouse;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data.MoonClothHouse
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DBContext _dbContext;

        public CustomerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }
        public async Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
            // return await _dbContext.Customers.FindAsync(id);
        }
        public async Task AddAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}


