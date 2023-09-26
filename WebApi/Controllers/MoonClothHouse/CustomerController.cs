using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.MoonClothHouse;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Domain.Models.MoonClothHouse.RequestModels;
using Domain.Repositories.MoonClothHouse;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.MoonClotHouse
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CustomerService _customerService;

        public CustomerController(IConfiguration configuration, CustomerService customerService)
        {
            _configuration = configuration;
            _customerService = customerService;
        }
        [HttpGet("profile")]
        [Authorize] // This attribute ensures that only authenticated users can access this endpoint
        public IActionResult UserProfile()
        {
            // Retrieve user claims from the token
            var claims = User.Claims;

            if (claims == null || !claims.Any())
            {
                // Claims are missing, or the user is not properly authenticated
                return Unauthorized();
            }

            // Create a dictionary to store user data
            var userData = new Dictionary<string, string>();

            foreach (var claim in claims)
            {
                userData[claim.Type] = claim.Value;
            }
            //if (userData.TryGetValue("CustomerId", out var customerId))
            //{
            //    // CustomerId is retrieved from the token
            //    return Ok(new { CustomerId = customerId });
            //}
            // Return user data as needed
            return Ok(userData);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid request");
            }

            // Authenticate the user
            var customer = await _customerService.GetUserByEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

            if (customer == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate a JWT token
            var token = GenerateJwtToken(customer);

            // Return the token and user data in the response
            return Ok(new
            {
                Token = token,
                User = new
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PasswordHash = customer.PasswordHash,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    PhoneNumber = customer.PhoneNumber,
                    CreatedAt = customer.CreatedAt,
                    UpdatedAt = customer.UpdatedAt,
                    // Add more user data properties as needed
                }
            });
        }

        private string GenerateJwtToken(Customer customer)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };

            // Add claims for all fields from the Customer model
            claims.AddRange(new[]
            {
                new Claim("CustomerId", customer.CustomerId),
                new Claim("FirstName", customer.FirstName),
                new Claim("LastName", customer.LastName),
                new Claim("Email", customer.Email),
                new Claim("PasswordHash", customer.PasswordHash),
                new Claim("Address", customer.Address),
                new Claim("City", customer.City),
                new Claim("State", customer.State),
                new Claim("ZipCode", customer.ZipCode),
                new Claim("PhoneNumber", customer.PhoneNumber),
                new Claim("CreatedAt", customer.CreatedAt?.ToString()),
                new Claim("UpdatedAt", customer.UpdatedAt?.ToString()),
                // Add more claims for additional fields as needed
            });

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


        private async Task<Customer> GetUser(string email, string password)
        {
            // Implement your user retrieval logic here
            // Example: Retrieve the user from the database based on email and password
            var user = await _customerService.GetUserByEmailAndPasswordAsync(email, password);
            return user;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // You can add additional validation logic here if needed

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