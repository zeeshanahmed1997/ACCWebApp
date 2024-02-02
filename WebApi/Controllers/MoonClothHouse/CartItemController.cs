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

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItemById(string id)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
                return NotFound();

            return Ok(cartItem);
        }




        //[HttpPost("CreateCartItem")]
        //public async Task<ActionResult<CartItem>> CreateCartItem(CartItem cartItem)
        //{
        //     await _cartItemService.AddCartItemAsync(cartItem);

        //    return CreatedAtAction(nameof(GetAllCartItems), null, cartItem);
        //}
        [HttpPost("CreateCartItem")]
        public async Task<ActionResult<CartItem>> CreateCartItem(CartItem cartItem)
        {
            try
            {
                // Check if both CartId and ProductId are provided
                if (string.IsNullOrEmpty(cartItem.CartId) || string.IsNullOrEmpty(cartItem.ProductId))
                {
                    return BadRequest("Both CartId and ProductId are required.");
                }

                // Add the cart item
                var createdCartItem = await _cartItemService.AddCartItemAsync(cartItem);

                // Return the created cart item with the correct route values
                return CreatedAtAction(nameof(GetCartItemById), new { id = createdCartItem.CartItemId }, createdCartItem);
            }
            catch (Exception e)
            {
                // Handle exception
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut("updateCartItem/{id}")]
        public async Task<ActionResult<CartItem>> UpdateCartItem(String id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
                return BadRequest();

            await _cartItemService.UpdateCartItemAsync(cartItem);
            return NoContent();
        }
        [HttpPut("updateCartItemCount")]
        public async Task<ActionResult<CartItem>> UpdateCartItemCount(int quantity, string cartItemId)
        {
            if (quantity == 0)
                return BadRequest();

            await _cartItemService.UpdateCartItemCountAsync(quantity, cartItemId);
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

