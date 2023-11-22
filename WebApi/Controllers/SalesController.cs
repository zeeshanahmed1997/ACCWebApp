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
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sales>>> GetAllSales()
        {
            var products = await _salesService.GetSalesAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSaleById(int id)
        {
            var product = await _salesService.GetSaleByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<ActionResult<Sales>> CreateSale(Sales sales)
        {
            await _salesService.AddSaleAsync(sales);
            return CreatedAtAction(nameof(GetSaleById), new { id = sales.SaleId }, sales);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Sales>> UpdateSale(int id, Sales sales)
        {
            if (id != sales.SaleId)
                return BadRequest();

            await _salesService.UpdateSaleAsync(sales);
            return NoContent();
        }
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(int id)
        {
            var product = await _salesService.GetSaleByIdAsync(id);
            if (product == null)
                return NotFound();

            await _salesService.DeleteSaleAsync(product);
            return NoContent();
        }
    }
}
