using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoonClothHous.Models.Products;
using System.Text;
using System.Net.Http.Headers;
using MoonClothHous.Models;
using MoonClothHous.Services;
using System.Net.Http;
using Domain.Models.MoonClothHouse;
using ProductImage = MoonClothHous.Models.Products.ProductImage;
using Product = MoonClothHous.Models.Products.Product;
using Newtonsoft.Json;
using MoonClothHous.Utilities;
using Microsoft.IdentityModel.Tokens;
using Utility = MoonClothHous.Utilities.Utility;
using Microsoft.AspNetCore.Authorization;

namespace MoonClothHous.Controllers.Products
{
    public class ProductsController : Controller
    {
        private readonly Utility utility;
        string productImageData = EndPoints.productsImageData;
        string baseURL = EndPoints.BaseURL;
        string products = EndPoints.productById;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(baseURL); // Set the correct base URL for your API
            _webHostEnvironment = webHostEnvironment;
            utility = new Utility(_httpClient);
        }

        public async Task<IActionResult> ProductsLandingPage()
        {
            List<Product> products = new List<Product>();
            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            var apiResponse = await _httpClient.GetAsync(productImageData);

            if (apiResponse.IsSuccessStatusCode)
            {
                var responseData = await apiResponse.Content.ReadAsStringAsync();
                var productImages = JsonConvert.DeserializeObject<List<ProductImage>>(responseData);
                foreach (ProductImage productImage in productImages)
                {
                    Product prod = await utility.FetchProductByIdAsync(productImage.ProductId);
                    products.Add(prod);
                }

                ViewData["Title"] = "Home Page";
                ViewData["UserName"] = userName; // Pass userName to the view
                ViewData["UserEmail"] = userEmail; // Pass userEmail to the view
                                                   //ViewData["ProductName"] = products;
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();

                for (int i = 0; i < products.Count && i < productImages.Count; i++)
                {
                    var product = products[i];
                    var productImage = productImages[i];

                    // Create a new ProductViewModel object
                    var productViewModel = new ProductViewModel
                    {
                        product = product,
                        productImage = productImage
                    };

                    // Add the new ProductViewModel object to the list
                    productViewModelList.Add(productViewModel);
                }
                return View(productViewModelList);
            }
            else
            {
                // Handle error case
                return View("Error");
            }
        }
        //[Authorize]
        public async Task<ActionResult> ProductDetailPageAsync(string id)
        {
            Product product = new Product();
            ProductImage productImage = new ProductImage();
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }

            try
            {
                product = await utility.FetchProductByIdAsync(id);
                if (product == null)
                {
                    return View("ProductNotFound");
                }

                productImage = await utility.FetchProductImageByIdAsync(id);
                if (productImage == null)
                {
                    return View("ProductImageNotFound");
                }

                return View("ProductDetailPage", new ProductViewModel
                {
                    product = product,
                    productImage = productImage
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
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
                        ProductId = "PRD00135",
                        ImageId = "IMG00006",
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
                        client.BaseAddress = new Uri("http://localhost:7241/");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Serialize the list of ProductImage objects to JSON
                        string productImagesJson = JsonConvert.SerializeObject(productImagesList);


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
