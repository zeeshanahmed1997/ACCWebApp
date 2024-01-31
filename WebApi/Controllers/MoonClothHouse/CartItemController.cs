using System;
using Application.Services.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.MoonClothHouse
{
    [ApiController]
    [Route("api/cartItem")]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemService _cartItemService;
        private readonly IWebHostEnvironment _env;
        public CartItemController(CartItemService cartItemService, IWebHostEnvironment env)
        {
            _env = env;
            _cartItemService = cartItemService;
        }

        [HttpGet("AllCartItems")]
        public async Task<ActionResult<List<CartItem>>> GetAllCartItems()
        {
            var cartItems = await _cartItemService.GetCartItemAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}", Name = "GetCartItemById")]
        public async Task<ActionResult<CartItem>> GetCartItemById(string id)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
                return NotFound();

            return Ok(cartItem);
        }




        [HttpPost("CreateCartItem")]
        public async Task<ActionResult<CartItem>> CreateCartItem(List<CartItem> cartItems)
        {
            foreach (var cartItem in cartItems)
            {
                await _cartItemService.AddCartItemAsync(cartItem);
            }

            return CreatedAtAction(nameof(GetAllCartItems), null, cartItems);
        }

        [HttpPut("updateCartItem/{id}")]
        public async Task<ActionResult<CartItem>> UpdateCartItem(String id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
                return BadRequest();

            await _cartItemService.UpdateCartItemAsync(cartItem);
            return NoContent();
        }

        [HttpDelete("deleteCartItem/{id}")]
        public async Task<ActionResult> DeleteCartItem(string id)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
                return NotFound();

            await _cartItemService.DeleteCartItemAsync(cartItem);
            return NoContent();
        }
    }
}

