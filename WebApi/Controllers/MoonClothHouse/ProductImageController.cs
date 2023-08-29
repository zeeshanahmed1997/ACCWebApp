using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.MoonClotHouse
{
    [ApiController]
    [Route("api/productImageData")]
    public class ProductImageController : ControllerBase
    {
        private readonly ProductImageService _productImageService;

        public ProductImageController(ProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductImage>>> GetAllProductImages()
        {
            var images = await _productImageService.GetProductImageAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImageById(int id)
        {
            var image = await _productImageService.GetProductImageIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
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
        public async Task<ActionResult> DeleteProductImage(int id)
        {
            var image = await _productImageService.GetProductImageIdAsync(id);
            if (image == null)
                return NotFound();

            await _productImageService.DeleteProductImageAsync(image);
            return NoContent();
        }
    }
}
