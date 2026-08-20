using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using MyPractice4.DTO;
using MyPractice4.Model;
using BCrypt.Net;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
        APIDbContext context, IConfiguration configuration)
        { 
        _context = context;
        _configuration= configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto) {
            var existingUser = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }


            var passwordHash = BCrypt.Net.BCrypt.HashPassword(
         dto.Password
     );
            var user = new User
            {
                Firstname=dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync ();
            var token = GenerateToken(user);

            return Ok(new
            {
                message = "Registration successful",
                userId = user.Id,
                email = user.Email,
                token = token
            });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto) {
            var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.Password
            );

            if (!passwordValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateToken(user);

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                email = user.Email,
                token = token
            });
        }




        // ==========================
        // GENERATE JWT
        // ==========================


        private object GenerateToken(User user)
        {

            var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()
            ),

            new Claim(
                ClaimTypes.Email,
                user.Email
            )
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
