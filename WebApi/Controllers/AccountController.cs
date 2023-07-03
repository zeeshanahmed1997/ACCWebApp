using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.WebApi.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;

        public AccountController(IConfiguration configuration, DBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginModel _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {

                    var userCategory = await GetUserCategory(user.UserId);
                    if (userCategory == _userData.Usercategory)
                    {
                        var token = GenerateJwtToken(user.Email, user.Password, userCategory);

                        return Ok(new { Token = token });
                    }
                    else
                    {
                        return BadRequest("Invalid User Category");
                    }
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private string GenerateJwtToken(string email, string password, string userCategory)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            const int requiredKeySizeInBytes = 32; // 256 bits

            if (key.Length < requiredKeySizeInBytes)
            {
                throw new ArgumentException($"Invalid key size. The key size must be {requiredKeySizeInBytes * 8} bits or larger.");
            }

            var notBefore = DateTime.UtcNow;
            var expires = notBefore.AddMinutes(10); // Set a fixed expiration time for the token, such as 10 minutes

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim("email", email),
            new Claim("password", password),
            new Claim("usercategory", userCategory)
        }),
                NotBefore = notBefore,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> GetUserCategory(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.Usercategory;
        }

        private async Task<Users> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
