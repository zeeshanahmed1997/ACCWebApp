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
    [Route("api/Fabric")]
    public class FabricController : ControllerBase
    {
        private readonly FabricService _fabricService;

        public FabricController(FabricService fabricService)
        {
            _fabricService = fabricService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fabric>>> GetAllFabric()
        {
            var products = await _fabricService.GetFabricAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fabric>> GetFabricById(int id)
        {
            var product = await _fabricService.GetFabricByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Fabric>> CreateFabric(Fabric clothing)
        {
            await _fabricService.AddFabricAsync(clothing);
            return CreatedAtAction(nameof(GetFabricById), new { id = clothing.FabricId }, clothing);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Fabric>> UpdateFabric(int id, Fabric clothing)
        {
            if (id != clothing.FabricId)
                return BadRequest();

            await _fabricService.UpdateFabricAsync(clothing);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFabric(int id)
        {
            var product = await _fabricService.GetFabricByIdAsync(id);
            if (product == null)
                return NotFound();

            await _fabricService.DeleteFabricAsync(product);
            return NoContent();
        }
    }
}
