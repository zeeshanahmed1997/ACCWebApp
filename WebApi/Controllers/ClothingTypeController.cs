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
    [Route("api/clothingType")]
    public class ClothingTypeController : ControllerBase
    {
        private readonly ClothingTypeService _clothingService;

        public ClothingTypeController(ClothingTypeService clothingService)
        {
            _clothingService = clothingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClothingType>>> GetAllClothes()
        {
            var products = await _clothingService.GetClothingTypeAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClothingType>> GetClothById(int id)
        {
            var product = await _clothingService.GetClothingTypeByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Clothing>> CreateProduct(ClothingType clothing)
        {
            await _clothingService.AddClothingTypeAsync(clothing);
            return CreatedAtAction(nameof(GetClothById), new { id = clothing.TypeId }, clothing);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClothingType>> UpdateCloth(int id,  ClothingType clothing)
        {
            if (id != clothing.TypeId)
                return BadRequest();

            await _clothingService.UpdateCothingTypeAsync(clothing);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCloth(int id)
        {
            var product = await _clothingService.GetClothingTypeByIdAsync(id);
            if (product == null)
                return NotFound();

            await _clothingService.DeleteClothingTypeAsync(product);
            return NoContent();
        }
    }
}
