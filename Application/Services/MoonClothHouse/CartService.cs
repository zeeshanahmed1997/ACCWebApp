using System;
using Domain.Models.MoonClothHouse;
using Domain.Repositories.MoonClothHouse;

namespace Application.Services.MoonClothHouse
{
	public class CartService
	{
        private readonly ICartRepository _cartRespository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRespository = cartRepository;
        }

        public async Task<IEnumerable<Cart>> GetCartAsync()
        {
            return await _cartRespository.GetAllAsync();
        }

        public async Task<Cart> GetCartIdAsync(string id)
        {
            Cart Cart = new Cart();
            Cart = await _cartRespository.GetByIdAsync(id);
            return Cart;
        }
        public async Task<Cart> GetCartByCustomerIdAsync(string id)
        {
            Cart Cart = new Cart();
            Cart = await _cartRespository.GetByCustomerId(id);
            return Cart;
        }

        public async Task AddCartAsync(Cart Cart)
        {
            await _cartRespository.AddAsync(Cart);
        }

        public async Task UpdateCartAsync(Cart Cart)
        {
            await _cartRespository.UpdateAsync(Cart);
        }

        public async Task DeleteCartAsync(Cart Cart)
        {
            await _cartRespository.DeleteAsync(Cart);
        }
    }
}

