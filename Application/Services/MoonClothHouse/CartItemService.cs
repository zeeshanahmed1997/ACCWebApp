using System;
using Domain.Models.MoonClothHouse;
using Domain.Repositories.MoonClothHouse;

namespace Application.Services.MoonClothHouse
{
	public class CartItemService
	{
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemAsync()
        {
            return await _cartItemRepository.GetAllAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(string id)
        {
            CartItem cartItem = new CartItem();
            cartItem = await _cartItemRepository.GetByIdAsync(id);
            return cartItem;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.AddAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.UpdateAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.DeleteAsync(cartItem);
        }
    }
}

