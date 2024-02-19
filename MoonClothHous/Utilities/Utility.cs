using System;
using MoonClothHous.Services;
using Newtonsoft.Json;
using System.Net.Http;
using MoonClothHous.Models.Products;

namespace MoonClothHous.Utilities
{
	public class Utility
	{
        string productImageData = EndPoints.productsImageData;
        string baseURL = EndPoints.BaseURL;
        string products = EndPoints.productById;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Utility(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseURL); // Set the correct base URL for your API
            _webHostEnvironment = webHostEnvironment;
        }
        public Utility(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Utility()
		{
		}
        public async Task<Product> FetchProductByIdAsync(string id)
        {
            try
            {
                string productUrl = $"{EndPoints.BaseURL}{EndPoints.productById.Replace("{id}", id)}";
                var apiResponse = await _httpClient.GetAsync(productUrl);

                if (!apiResponse.IsSuccessStatusCode)
                {
                    return null;
                }

                string productData = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(productData, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                });
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the HTTP request exception
                Console.WriteLine($"HTTP request exception: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
            catch (JsonException ex)
            {
                // Log or handle the JSON deserialization exception
                Console.WriteLine($"JSON deserialization exception: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"An unexpected exception occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<ProductImage> FetchProductImageByIdAsync(string id)
        {
            string productImageById = $"{EndPoints.BaseURL}{EndPoints.productImageById.Replace("{id}", id)}";
            var productImageResponse = await _httpClient.GetAsync(productImageById);

            if (!productImageResponse.IsSuccessStatusCode)
            {
                return null;
            }

            string productImageData = await productImageResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductImage>(productImageData);
        }
    }
}

