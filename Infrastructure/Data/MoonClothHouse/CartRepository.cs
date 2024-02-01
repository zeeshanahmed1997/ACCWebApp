using System;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using Domain.Repositories.MoonClothHouse;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.MoonClothHouse
{
    public class CartRepository : ICartRepository
    {
        private readonly DBContext _dbContext;

        public CartRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            var carts = await _dbContext.Carts.ToListAsync();

            return carts;
        }


        public async Task<Cart> GetByIdAsync(string cartId)
        {
            Cart cart = await _dbContext.Carts
                .FirstOrDefaultAsync(p => p.CartId == cartId);
            return cart;
        }

        public async Task AddAsync(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _dbContext.Entry(cart).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart cart)
        {
            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();
        }

        Task<Cart> IMoonClothHouseRepository<Cart>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<Cart> ICartRepository.GetByCustomerId(string customerId)
        {
            Cart cart = await _dbContext.Carts
                .FirstOrDefaultAsync(p => p.CustomerId == customerId);
            return cart;
        }
    }
}