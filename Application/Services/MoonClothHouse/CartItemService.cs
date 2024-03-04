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

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await _cartItemRepository.AddAsync(cartItem);
            return cartItem;
        }


        public async Task UpdateCartItemCountAsync(int quantity, string cartItemId)
        {
            // Retrieve the cart item from the repository using the cartItemId
            CartItem cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);

            if (cartItem != null)
            {
                // Update the quantity
                cartItem.Quantity = cartItem.Quantity+quantity;

                // Save the updated cart item
                await _cartItemRepository.UpdateAsync(cartItem);
            }
            else
            {
                throw new Exception("Cart item not found.");
            }
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

