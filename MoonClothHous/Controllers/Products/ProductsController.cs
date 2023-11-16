using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonClothHous.Models.Products;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Grpc.Core;
using System.Net.Http.Headers;
using MoonClothHous.Models;

namespace MoonClothHous.Controllers.Products
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7241"); // Set the correct base URL for your API
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> ProductsLandingPage()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            var apiResponse = await _httpClient.GetAsync("http://localhost:7241/api/productImageData");

            if (apiResponse.IsSuccessStatusCode)
            {
                var responseData = await apiResponse.Content.ReadAsStringAsync();
                var productImages = JsonSerializer.Deserialize<List<ProductImage>>(responseData);

                ViewData["Title"] = "Home Page";
                ViewData["Title"] = "Home Page";
                ViewData["UserName"] = userName; // Pass userName to the view
                ViewData["UserEmail"] = userEmail; // Pass userEmail to the view

                return View(productImages);
            }
            else
            {
                // Handle error case
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] ProductViewModel model)
        {
            try
            {
                return Json(new { success = true, message = "Product added to cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public IActionResult Upload()
        {
            var file = Request.Form.Files["videoFile"]; // Access the uploaded file using the form field name

            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Json(new { message = "File uploaded successfully!" });
                }
                catch
                {
                    return Json(new { message = "Error uploading the file." });
                }
            }
            else
            {
                return Json(new { message = "Please select a file to upload." });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private const string ImagesFolder = "~/images/";

        private string GetImagesFolderPath()
        {
            string imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }
            return imagesFolder;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Test(IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(GetImagesFolderPath(), filename);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Construct the image URL
                    string imageUrl = "/" + Path.Combine("images", filename).Replace('\\', '/');
                    ViewBag.ImageUrl = imageUrl;

                    // Create a ProductImage object
                    ProductImage productImage = new ProductImage
                    {
                        ImageUrl = imageUrl,
                        IsPrimary = false,
                        ProductId = "PRD00005",
                        ImageId = "IMG00001",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    // Prepare a list of ProductImage objects for the API
                    List<ProductImage> productImagesList = new List<ProductImage>
            {
                productImage // Add more objects to the list if needed
            };

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7241/");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Serialize the list of ProductImage objects to JSON
                        string productImagesJson = JsonSerializer.Serialize(productImagesList);

                        // Create the HTTP content
                        HttpContent content = new StringContent(productImagesJson, Encoding.UTF8, "application/json");

                        // Send the POST request manually
                        HttpResponseMessage response = await client.PostAsync("api/productImageData", content);

                        // Log the response status code
                        Console.WriteLine($"Response Status Code: {response.StatusCode}");

                        if (response.IsSuccessStatusCode)
                        {
                            // Images stored successfully in the database
                            return RedirectToAction("ProductsLandingPage", "Products");
                        }
                        else
                        {
                            // Handle API error
                            ViewBag.ErrorMessage = "Error storing images.";
                            return View("Test");
                        }
                    }
                }

                ViewBag.ErrorMessage = "No image or invalid image.";
                return View("Privacy");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("ImageUploadFailure");
            }
        }

    }
}
