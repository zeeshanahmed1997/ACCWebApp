using System;
using Application.Services.MoonClothHouse;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers.MoonClothHouse
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        public static string? CustomerId;
        private readonly CartItemController _cartItemController;
        private readonly CartService _cartService;
        private readonly IWebHostEnvironment _env;
        public CartController(CartService cartService, CartItemController cartItemController, IWebHostEnvironment env)
        {
            _env = env;
            _cartItemController = cartItemController;
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

        [HttpGet("customer")]
        [Authorize]
        public async Task<ActionResult<Cart>> GetCartByCustomerId(string customerId)
        {
            //Retrieve user claims from the token
            var claims = User.Claims;

            // Extract CustomerId claim from user claims
            var customerIdClaim = claims.FirstOrDefault(c => c.Type == "CustomerId");
            //customerId = customerIdClaim;
            if (customerIdClaim == null)
                return Unauthorized();

            //var customerId = customerIdClaim.Value;

            var cart = await _cartService.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }
        [HttpPost("CreateCart")]
        public async Task<ActionResult<Cart>> CreateCart(CartViewModel cartViewModel)
        {
            try
            {
                // Check if customer's cart exists
                ActionResult<Cart> customerCartResult = await GetCartByCustomerId(cartViewModel.CustomerId);
                if (customerCartResult.Result is OkObjectResult)
                {
                    // Customer's cart exists, update cart item count
                    Cart customerCart = (customerCartResult.Result as OkObjectResult)?.Value as Cart;
                    ActionResult<List<CartItem>> cartItemResult = await _cartItemController.GetAllCartItems();
                    if (cartItemResult.Result is OkObjectResult)
                    {
                        List<CartItem> cartItems = (cartItemResult.Result as OkObjectResult)?.Value as List<CartItem>;
                        List<CartItem> matchedCartItems = cartItems.Where(item => item.CartId == customerCart.CartId).ToList();
                        if (matchedCartItems != null)
                        {
                            CartItem matchedProduct = matchedCartItems.FirstOrDefault(item => item.ProductId == cartViewModel.ProductId);
                            if (matchedProduct != null)
                            {
                                ActionResult<CartItem> updatedCartItem = await _cartItemController.UpdateCartItemCount(cartViewModel.Quantity, matchedProduct.CartItemId);
                                if (updatedCartItem.Result is NoContentResult)
                                {
                                    // Cart item count updated successfully
                                    return Ok("Cart item count updated successfully");
                                }
                                else
                                {
                                    return StatusCode(500, "Error updating cart item count");
                                }
                            }
                            else
                            {
                                ActionResult<List<CartItem>> cartItemsList = await _cartItemController.GetAllCartItems();
                                if (cartItemsList.Result is OkObjectResult)
                                {
                                    List<CartItem> cartItemList = (cartItemsList.Result as OkObjectResult)?.Value as List<CartItem>;

                                    int maxCartItemId = cartItems.Any() ? cartItems.Max(ci => int.Parse(ci.CartItemId.Substring(5))) : 0;
                                    string newCartItemId = $"CITEM{(maxCartItemId + 1):00000}";
                                    CartItem cartItem = new CartItem()
                                    {
                                        CartId = customerCart.CartId,
                                        CartItemId = newCartItemId,
                                        PricePerUnit = 49,
                                        ProductId = cartViewModel.ProductId,
                                        Quantity = cartViewModel.Quantity,
                                        CreatedAt = cartViewModel.CreatedAt,
                                        UpdatedAt = cartViewModel.UpdatedAt
                                    };
                                    ActionResult<CartItem> cartResult = await _cartItemController.CreateCartItem(cartItem);

                                    if (cartResult.Result is OkObjectResult okObjectResult && okObjectResult.Value is string message && message == "Item added to cart successfully")
                                    {
                                        // The cart item was added successfully
                                        return Ok("Cart item count updated successfully");
                                    }


                                }
                                return NotFound("Product Not Added To Cart");
                            }
                        }
                        else
                        {
                            // No cart items found
                            return NotFound("No cart items found");
                        }
                    }
                    else
                    {
                        // Error retrieving cart items
                        return StatusCode(500, "Error retrieving cart items");
                    }
                }
                else
                {
                    // If customer's cart doesn't exist, create a new cart
                    ActionResult<List<Cart>> carts = await GetAllCarts();
                    if (carts.Result is OkObjectResult)
                    {
                        List<Cart> createCart = (carts.Result as OkObjectResult)?.Value as List<Cart>;
                        int maxCartId = createCart.Any() ? createCart.Max(c => int.Parse(c.CartId.Substring(4))) : 0;
                        string newCartId = $"CART{(maxCartId + 1):00000}";

                        Cart cart = new Cart()
                        {
                            CartId = newCartId,
                            CreatedAt = cartViewModel.CreatedAt,
                            CustomerId = cartViewModel.CustomerId,
                            UpdatedAt = cartViewModel.UpdatedAt,
                        };

                        // Add the new cart
                        await _cartService.AddCartAsync(cart);

                        // Get all cart items
                        ActionResult<List<CartItem>> cartItemsList = await _cartItemController.GetAllCartItems();
                        if (cartItemsList.Result is OkObjectResult)
                        {
                            List<CartItem> cartItems = (cartItemsList.Result as OkObjectResult)?.Value as List<CartItem>;

                            int maxCartItemId = cartItems.Any() ? cartItems.Max(ci => int.Parse(ci.CartItemId.Substring(5))) : 0;
                            string newCartItemId = $"CITEM{(maxCartItemId + 1):00000}";

                            CartItem cartItem = new CartItem()
                            {
                                CartItemId = newCartItemId,
                                ProductId = cartViewModel.ProductId,
                                CartId = cart.CartId,
                                Quantity = cartViewModel.Quantity,
                                CreatedAt = cartViewModel.CreatedAt,
                                UpdatedAt = cartViewModel.UpdatedAt,
                                PricePerUnit = 49,
                            };

                            // Create new cart item
                            ActionResult<CartItem> cartItemResult = await _cartItemController.CreateCartItem(cartItem);
                            if (cartItemResult.Result is OkObjectResult okObjectResult && okObjectResult.Value is string message && message == "Item added to cart successfully")
                            {
                                // The cart item was added successfully
                                return Ok("Cart item count updated successfully");
                            }
                            else
                            {
                                // Error creating cart item
                                return StatusCode(500, "Error creating cart item");
                            }
                        }
                        else
                        {
                            // Error retrieving cart items
                            return StatusCode(500, "Error retrieving cart items");
                        }
                    }
                    else
                    {
                        // Error retrieving carts
                        return StatusCode(500, "Error retrieving carts");
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any unhandled exceptions
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }



        //[HttpPost("CreateCart")]
        //public async Task<ActionResult<Cart>> CreateCart(CartViewModel cartViewModel)
        //{
        //    try
        //    {
        //        // Check if customer's cart exists
        //        ActionResult<Cart> customerCartResult = await GetCartByCustomerId();
        //        if (!(customerCartResult.Result is OkResult))
        //        {
        //            // If customer's cart doesn't exist, create a new cart
        //            ActionResult<List<Cart>> carts = await GetAllCarts();
        //            if (carts.Result is OkObjectResult)
        //            {
        //                List<Cart> createCart = (carts.Result as OkObjectResult)?.Value as List<Cart>;
        //                int maxCartId = createCart.Any() ? createCart.Max(c => int.Parse(c.CartId.Substring(4))) : 0;
        //                string newCartId = $"CART{(maxCartId + 1):00000}";

        //                Cart cart = new Cart()
        //                {
        //                    CartId = newCartId,
        //                    CreatedAt = cartViewModel.CreatedAt,
        //                    CustomerId = cartViewModel.CustomerId,
        //                    UpdatedAt = cartViewModel.UpdatedAt,
        //                };

        //                // Add the new cart
        //                await _cartService.AddCartAsync(cart);

        //                // Get all cart items
        //                ActionResult<List<CartItem>> cartItemsList = await _cartItemController.GetAllCartItems();
        //                if (cartItemsList.Result is OkObjectResult)
        //                {
        //                    List<CartItem> cartItems = (cartItemsList.Result as OkObjectResult)?.Value as List<CartItem>;

        //                    int maxCartItemId = cartItems.Any() ? cartItems.Max(ci => int.Parse(ci.CartItemId.Substring(5))) : 0;
        //                    string newCartItemId = $"CITEM{(maxCartItemId + 1):00000}";

        //                    CartItem cartItem = new CartItem()
        //                    {
        //                        CartItemId = newCartItemId,
        //                        ProductId = cartViewModel.ProductId,
        //                        CartId = cart.CartId,
        //                        Quantity = cartViewModel.Quantity,
        //                        CreatedAt = cartViewModel.CreatedAt,
        //                        UpdatedAt = cartViewModel.UpdatedAt,
        //                        PricePerUnit = 49,
        //                    };

        //                    // Create new cart item
        //                    ActionResult<CartItem> cartItemResult = await _cartItemController.CreateCartItem(cartItem);
        //                    if (cartItemResult.Result is OkResult)
        //                    {
        //                        // Cart item created successfully, return the cart
        //                        return cart;
        //                    }
        //                    else
        //                    {
        //                        // Error creating cart item
        //                        return StatusCode(500, "Error creating cart item");
        //                    }
        //                }
        //                else
        //                {
        //                    // Error retrieving cart items
        //                    return StatusCode(500, "Error retrieving cart items");
        //                }
        //            }
        //            else
        //            {
        //                // Error retrieving carts
        //                return StatusCode(500, "Error retrieving carts");
        //            }
        //        }
        //        else
        //        {
        //            // Customer's cart exists, update cart item count
        //            Cart customerCart = (customerCartResult.Result as OkObjectResult)?.Value as Cart;
        //            ActionResult<List<CartItem>> cartItemResult = await _cartItemController.GetAllCartItems();
        //            if (cartItemResult.Result is OkObjectResult)
        //            {
        //                List<CartItem> cartItems = (cartItemResult.Result as OkObjectResult)?.Value as List<CartItem>;
        //                List<CartItem> matchedCartItems = cartItems.Where(item => item.CartId == customerCart.CartId).ToList();
        //                if (matchedCartItems != null)
        //                {
        //                    CartItem matchedProduct = matchedCartItems.FirstOrDefault(item => item.ProductId == cartViewModel.ProductId);
        //                    if (matchedProduct != null)
        //                    {
        //                        ActionResult<CartItem> updatedCartItem = await _cartItemController.UpdateCartItemCount(cartViewModel.Quantity, matchedProduct.ProductId);
        //                        if (updatedCartItem.Result is OkResult)
        //                        {
        //                            // Cart item count updated successfully
        //                            return CreatedAtAction(nameof(customerCart), null, cartViewModel);
        //                        }
        //                        else
        //                        {
        //                            // Error updating cart item count
        //                            return StatusCode(500, "Error updating cart item count");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Cart item not found
        //                        return NotFound("Cart item not found");
        //                    }
        //                }
        //                else
        //                {
        //                    // No cart items found
        //                    return NotFound("No cart items found");
        //                }
        //            }
        //            else
        //            {
        //                // Error retrieving cart items
        //                return StatusCode(500, "Error retrieving cart items");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Catch any unhandled exceptions
        //        return StatusCode(500, "An error occurred: " + ex.Message);
        //    }
        //}

        //[HttpPost("CreateCart")]
        //public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        //{
        //    await _cartService.AddCartAsync(cart);

        //    return CreatedAtAction(nameof(GetAllCarts), null, cart);
        //}
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

