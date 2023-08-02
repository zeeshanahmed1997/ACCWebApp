using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.MoonClotHouse
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var gender = await _customerService.GetCustomersAsync();
            return Ok(gender);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        //{
        //    if (id != customer.CustomerId)
        //        return BadRequest();

        //    await _customerService.UpdateCustomerAsync(customer);
        //    return NoContent();
        //}
        ///
        ///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var gender = await _customerService.GetCustomerByIdAsync(id);
            if (gender == null)
                return NotFound();

            await _customerService.DeleteCustomerAsync(gender);
            return NoContent();
        }
    }
}
