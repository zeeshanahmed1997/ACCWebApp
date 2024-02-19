using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/clothes")]
    public class ClothingController : ControllerBase
    {
        private readonly ClothingService _clothingService;

        public ClothingController(ClothingService clothingService)
        {
            _clothingService = clothingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clothing>>> GetAllClothes()
        {
            var products = await _clothingService.GetClothesAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Clothing>> GetClothById(int id)
        {
            var product = await _clothingService.GetClothByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Clothing>> CreateProduct(Clothing clothing)
        {
            await _clothingService.AddClothAsync(clothing);
            return CreatedAtAction(nameof(GetClothById), new { id = clothing.ClothingId }, clothing);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Clothing>> UpdateCloth(int id, Clothing clothing)
        {
            if (id != clothing.ClothingId)
                return BadRequest();

            await _clothingService.UpdateProductAsync(clothing);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCloth(int id)
        {
            var product = await _clothingService.GetClothByIdAsync(id);
            if (product == null)
                return NotFound();

            await _clothingService.DeleteProductAsync(product);
            return NoContent();
        }
    }
}
