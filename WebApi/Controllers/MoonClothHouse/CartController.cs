using System;
using Application.Services.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.MoonClothHouse
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly IWebHostEnvironment _env;
        public CartController(CartService cartService, IWebHostEnvironment env)
        {
            _env = env;
            _cartService = cartService;
        }

        [HttpGet("AllCarts")]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            var cartItems = await _cartService.GetCartAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}", Name = "GetCartById")]
        public async Task<ActionResult<Cart>> GetCartById(string id)
        {
            var cart = await _cartService.GetCartIdAsync(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<Cart>> GetCartByCustomerId(string customerId)
        {
            var cart = await _cartService.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("CreateCart")]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            await _cartService.AddCartAsync(cart);

            return CreatedAtAction(nameof(GetAllCarts), null, cart);
        }

        [HttpPut("updateCart/{id}")]
        public async Task<ActionResult<Cart>> UpdateCart(String id, Cart cart)
        {
            if (id != cart.CartId)
                return BadRequest();

            await _cartService.UpdateCartAsync(cart);
            return NoContent();
        }

        [HttpDelete("deleteCart/{id}")]
        public async Task<ActionResult> DeleteCart(string id)
        {
            var cart = await _cartService.GetCartIdAsync(id);
            if (cart == null)
                return NotFound();

            await _cartService.DeleteCartAsync(cart);
            return NoContent();
        }
    }
}

