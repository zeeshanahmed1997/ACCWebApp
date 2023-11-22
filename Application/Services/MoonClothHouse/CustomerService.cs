using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Domain.Repositories;
using Domain.Repositories.MoonClothHouse;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.MoonClothHouse
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly DBContext _dbContext;

        public CustomerService(ICustomerRepository customerRepository, DBContext dBContext)
        {
            _dbContext = dBContext;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task<Customer> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            // Implement your user retrieval logic here
            // This is a sample implementation using Entity Framework Core for demonstration purposes

            // Hash the provided password (replace with your actual password hashing logic)
//            string hashedPassword = HashPassword(password);

            // Retrieve the user by email and hashed password
            var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

            return user;
        }

        // Sample password hashing method (replace with your actual password hashing mechanism)
        private string HashPassword(string password)
        {
            // Implement your password hashing logic here (e.g., using BCrypt or another secure hashing library)
            // For demonstration purposes, this is a simple example using SHA256 hashing
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}


