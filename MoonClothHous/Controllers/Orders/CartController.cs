using System.Net;
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
            if (quantity <= 0 || string.IsNullOrEmpty(productId))
            {
                // Handle invalid input
                return BadRequest("Invalid quantity or product id.");
            }

            try
            {
                string customerId = HttpContext.Session.GetString("CustomerId");
                CartModel cart = await GetCartByCustomerId(customerId);

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseURL);

                    List<CartItem> cartItems = await GetAllCartItemsAsync();

                    // Find the cart item with the highest CartItemId
                    int maxCartItemId = cartItems.Any() ? cartItems.Max(ci => int.Parse(ci.CartItemId.Substring(5))) : 0;

                    // Generate new cart_item_id in the specified format
                    string newCartItemId = $"CITEM{(maxCartItemId + 1):00000}";

                    // Create a new CartItem
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
                    HttpResponseMessage cartItemResponse = await httpClient.PostAsync(addToCartItem, cartItemContent);

                    if (cartItemResponse.IsSuccessStatusCode)
                    {
                        // Handle success response if needed
                        return View("Cart and cart item added successfully.");
                    }
                    else
                    {
                        // Handle error response for adding cart item
                        return StatusCode((int)cartItemResponse.StatusCode, "Error adding cart item.");
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exception
                return StatusCode(500, "Internal server error.");
            }
        }

        //private async Task<CartModel> CreateCart(string customerId)
        //{
        //    CartModel newCart=new CartModel();
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            List<Cart> createCart = await GetAllCartsAsync();

        //            // Find the cart with the highest CartId
        //            int maxCartId = createCart.Any() ? createCart.Max(c => int.Parse(c.CartId.Substring(4))) : 0;

        //            // Generate new cart_id in the specified format
        //            string newCartId = $"CART{(maxCartId + 1):00000}";

        //            newCart = new CartModel()
        //            {
        //                CartId = newCartId,
        //                CustomerId = customerId,
        //                CreatedAt = DateTime.Now,
        //                UpdatedAt = DateTime.Now
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return newCart;
        //}

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
                        // If the request failed, handle the error (e.g., log, throw exception, etc.)
                        // For example:
                        // throw new Exception($"Failed to create cart. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw)
            }
            return newCart;
        }

        private async Task<CartModel> GetCartByCustomerId(string customerId)
        {
            CartModel cart = null;
            Cart carts = null;
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
                    // Handle other non-success status codes if needed
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

