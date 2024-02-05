﻿using System.Net;
using System.Text;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;
using MoonClothHous.Models.Accounts;
using MoonClothHous.Models.Orders;
using MoonClothHous.Services;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoonClothHous.Controllers.Orders
{
    public class CartController : Controller
    {

        string baseURL = EndPoints.BaseURL;
        string addToCart = EndPoints.AddToCart;
        string getAllCarts = EndPoints.GetAllCarts;
        string getAllCartItems = EndPoints.GetAllCartItems;
        string addToCartItem = EndPoints.AddToCartItem;
        string getCartByCustomerId = EndPoints.GetCartByCustomerId;
        string updateCartItemCount = EndPoints.UpdateCartItemCount;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CartController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseURL); // Set the correct base URL for your API
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: /<controller>/
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(int quantity, string productId)
        {
            try
            {
                if (!IsValidInput(quantity, productId))
                {
                    return BadRequest("Invalid quantity or product id.");
                }

                CartModel cart = await GetCustomerCartAsync();

                //If user is not logged in and cart returned is null then we need to store the cart in local storage or session storage
                if (cart == null) // User not logged in
                {
                    // Create a new cart object and cart items
                    cart = new CartModel();
                    List<CartItem> cartItems = new List<CartItem>(); // Initialize an empty list of cart items

                    // Populate the cart and cart items with the provided quantity and product ID
                    CartItem newCartItem = new CartItem
                    {
                        Quantity = quantity,
                        ProductId = productId,
                        CartItemId = null,
                        CartId = null,
                        PricePerUnit = 0

                        // Add any other necessary properties
                    };
                    cartItems.Add(newCartItem);

                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                    HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(cartItems));


                    return Ok("Cart and cart items saved locally.");
                }


                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);

                    List<CartItem> cartItems = await GetAllCartItemsAsync();

                    CartItem matchingItem = FindMatchingCartItem(cartItems, cart.CartId, productId);

                    if (matchingItem != null)
                    {
                        await UpdateCartItemQuantityAsync(httpClient, matchingItem, quantity);

                        return Ok("Quantity updated successfully.");
                    }
                    else
                    {
                        await CreateNewCartItemAsync(httpClient, cart, quantity, productId);

                        return Ok("Cart and cart item added successfully.");
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exception
                return StatusCode(500, "Internal server error.");
            }
        }


        private bool IsValidInput(int quantity, string productId)
        {
            return quantity > 0 && !string.IsNullOrEmpty(productId);
        }

        private async Task<CartModel> GetCustomerCartAsync()
        {
            string? customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return null;
            }
            else
            {
                return await GetCartByCustomerId(customerId);
            }
        }

        private CartItem FindMatchingCartItem(List<CartItem> cartItems, string cartId, string productId)
        {
            return cartItems.Find(item => item.CartId == cartId && item.ProductId == productId);
        }

        private async Task UpdateCartItemQuantityAsync(HttpClient httpClient, CartItem matchingItem, int quantity)
        {
            matchingItem.Quantity += quantity;

            var jsonCartItem = JsonConvert.SerializeObject(matchingItem);
            var cartItemContent = new StringContent(jsonCartItem, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"{updateCartItemCount}?quantity={quantity}&cartItemId={matchingItem.CartItemId}", cartItemContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating cart item.");
            }
        }

        private async Task CreateNewCartItemAsync(HttpClient httpClient, CartModel cart, int quantity, string productId)
        {
            List<CartItem> cartItems = await GetAllCartItemsAsync();

            int maxCartItemId = cartItems.Any() ? cartItems.Max(ci => int.Parse(ci.CartItemId.Substring(5))) : 0;
            string newCartItemId = $"CITEM{(maxCartItemId + 1):00000}";

            CartItemModel cartItem = new CartItemModel()
            {
                CartItemId = newCartItemId,
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity,
                PricePerUnit = 49.9,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var jsonCartItem = JsonConvert.SerializeObject(cartItem);
            var cartItemContent = new StringContent(jsonCartItem, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(addToCartItem, cartItemContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error adding cart item.");
            }
        }

        private async Task<CartModel> CreateCart(string customerId)
        {
            CartModel newCart = new CartModel();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);

                    List<Cart> createCart = await GetAllCartsAsync();
                    int maxCartId = createCart.Any() ? createCart.Max(c => int.Parse(c.CartId.Substring(4))) : 0;
                    string newCartId = $"CART{(maxCartId + 1):00000}";
                    newCart = new CartModel()
                    {
                        CartId = newCartId,
                        CustomerId = customerId,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    // Serialize newCart to JSON
                    string newCartJson = JsonConvert.SerializeObject(newCart);
                    var cartItemContent = new StringContent(newCartJson, Encoding.UTF8, "application/json");
                    // Send HTTP POST request to create a new cart
                    HttpResponseMessage response = await httpClient.PostAsync(addToCart, cartItemContent);

                    if (response.IsSuccessStatusCode)
                    {
                        // If the request was successful, read the response content
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        newCart = JsonConvert.DeserializeObject<CartModel>(apiResponse);
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return newCart;
        }

        // It will return cart for user if exists otherwise will create it
        private async Task<CartModel> GetCartByCustomerId(string customerId)
        {
            CartModel cart = new CartModel();
            Cart carts = new Cart();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);
                    string endpoint = $"/api/cart/customer/{customerId}"; // Assuming the API endpoint follows this pattern
                    HttpResponseMessage response = await httpClient.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        carts = JsonConvert.DeserializeObject<Cart>(apiResponse);
                        cart = new CartModel()
                        {
                            CartId = carts.CartId,
                            CustomerId = carts.CustomerId,
                            CreatedAt = (DateTime)carts.CreatedAt,
                            UpdatedAt = (DateTime)carts.UpdatedAt,
                        };
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {

                        cart = await CreateCart(customerId);
                        return cart;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return cart;
        }

        // Method to get all carts from API
        private async Task<List<Cart>> GetAllCartsAsync()
        {
            List<Cart> carts = new List<Cart>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);
                    HttpResponseMessage response = await httpClient.GetAsync(getAllCarts);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        carts = JsonConvert.DeserializeObject<List<Cart>>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return carts;
        }
        // Method to get all cart items from API
        private async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            List<CartItem> cartItems = new List<CartItem>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);
                    HttpResponseMessage response = await httpClient.GetAsync(getAllCartItems);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cartItems = JsonConvert.DeserializeObject<List<CartItem>>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return cartItems;
        }
    }
}

