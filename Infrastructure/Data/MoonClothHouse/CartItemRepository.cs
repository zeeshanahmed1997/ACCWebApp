using System;
using Domain.Interfaces.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using Domain.Repositories.MoonClothHouse;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.MoonClothHouse
{
	public class CartItemRepository:ICartItemRepository
	{
        private readonly DBContext _dbContext;

        public CartItemRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            var cartItems = await _dbContext.CartItems.ToListAsync();

            return cartItems;
        }


        public async Task<CartItem> GetByIdAsync(string cartItemId)
        {
            CartItem cartItem = await _dbContext.CartItems
                .FirstOrDefaultAsync(p => p.CartItemId == cartItemId);
            return cartItem;
        }

        public async Task AddAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            _dbContext.Entry(cartItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        Task<CartItem> IMoonClothHouseRepository<CartItem>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

