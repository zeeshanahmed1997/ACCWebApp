using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonClothHous.Models;
using System.IO;
using System.Text.Json;

namespace MoonClothHous.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var apiResponse = await httpClient.GetAsync("https://localhost:7241/api/productImageData");

            if (apiResponse.IsSuccessStatusCode)
            {
                var responseData = await apiResponse.Content.ReadAsStringAsync();
                var productImages = JsonSerializer.Deserialize<List<ProductImage>>(responseData);

                ViewData["Title"] = "Home Page";
                return View(productImages);
            }
            else
            {
                // Handle error case
                return View("Error");
            }
        }
        public IActionResult Privacy()
        {
            return View();
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
    }
}
