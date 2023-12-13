using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApi.Controllers.MoonClotHouse
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _env;
        public ProductController(ProductService productService, IWebHostEnvironment env)
        {
            _env = env;
            _productService = productService;
        }

        [HttpGet("AllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var images = await _productService.GetProductAsync();
            return Ok(images);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductImage>> GetProductImageById(string id)
        //{
        //    var image = await _productImageService.GetProductImageIdAsync(id);
        //    if (image == null)
        //        return NotFound();

        //    return Ok(image);
        //}
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var productImage = await _productService.GetProductByIdAsync(id);
                if (productImage == null)
                    return NotFound("Image not found.");

                //var imageUrl = productImage.ImageUrl;
                var imageUrl = "";
                // Ensure the ImageUrl begins with a relative path
                imageUrl = imageUrl.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                // Combine the paths to create an absolute path to the image file
                var imagePath = Path.Combine(_env.WebRootPath, imageUrl);

                if (!System.IO.File.Exists(imagePath))
                    return NotFound("File not found.");

                // Extract the file extension and use it to determine the MIME type
                var contentType = "application/octet-stream"; // Default MIME type if not identified
                new FileExtensionContentTypeProvider().TryGetContentType(imagePath, out contentType);

                // The above provider will return false if no MIME type is found, in which case we set a default
                if (contentType == null)
                    contentType = "application/octet-stream";

                var bytes = await System.IO.File.ReadAllBytesAsync(imagePath);
                return File(bytes, contentType);
            }
            catch (Exception ex)
            {
                // Log the exception message using proper logging mechanism
                // For example: _logger.LogError(ex, "An error occurred while getting the product image.");
                return StatusCode(500, "An internal error occurred.");
            }
        }




        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct(List<Product> products)
        {
            foreach (var productImage in products)
            {
                await _productService.AddProductAsync(productImage);
            }

            return CreatedAtAction(nameof(GetAllProducts), null, products);
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(String id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var image = await _productService.GetProductByIdAsync(id);
            if (image == null)
                return NotFound();

            await _productService.DeleteProductAsync(image);
            return NoContent();
        }
    }
}
