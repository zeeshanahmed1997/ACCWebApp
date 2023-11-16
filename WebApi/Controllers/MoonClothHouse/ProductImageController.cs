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
    [Route("api/productImageData")]
    public class ProductImageController : ControllerBase
    {
        private readonly ProductImageService _productImageService;
        private readonly IWebHostEnvironment _env;
        public ProductImageController(ProductImageService productImageService, IWebHostEnvironment env)
        {
            _env = env;
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductImage>>> GetAllProductImages()
        {
            var images = await _productImageService.GetProductImageAsync();
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            try
            {
                var productImage = await _productImageService.GetProductImageIdAsync(id);
                if (productImage == null)
                    return NotFound("Image not found.");

                var imageUrl = productImage.ImageUrl;

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




        [HttpPost]
        public async Task<ActionResult<ProductImage>> CreateProductImages(List<ProductImage> productImages)
        {
            foreach (var productImage in productImages)
            {
                await _productImageService.AddProductImageAsync(productImage);
            }

            return CreatedAtAction(nameof(GetAllProductImages), null, productImages);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductImage>> UpdateProductImage(String id, ProductImage productImage)
        {
            if (id != productImage.ImageId)
                return BadRequest();

            await _productImageService.UpdateProductImageAsync(productImage);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductImage(string id)
        {
            var image = await _productImageService.GetProductImageIdAsync(id);
            if (image == null)
                return NotFound();

            await _productImageService.DeleteProductImageAsync(image);
            return NoContent();
        }
    }
}
