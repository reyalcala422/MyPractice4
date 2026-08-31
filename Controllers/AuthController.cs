using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyPractice4.Data;
using MyPractice4.DTO;
using MyPractice4.DTO.Place;
using MyPractice4.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

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



        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> PutPlace(int id, UpdateUserPlace dto)
        {
            var user = await _context.Users
                .Include(u => u.UserPlaces)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            _context.UserPlaces.RemoveRange(user.UserPlaces);

            await _context.SaveChangesAsync();

            foreach (var placeId in dto.PlaceId.Distinct())
            {
                var userPlace = new UserPlaces
                {
                    UserId = id,
                    PlaceId = placeId
                };
                _context.UserPlaces.Add(userPlace);
            }

            await _context.SaveChangesAsync();

            var result = await _context.Users
                .Include(x => x.UserPlaces)
                .ThenInclude(x => x.Place)
                .Where(x => x.Id == id)
                .Select(x => new { 
                x.Id,x.Firstname,x.Lastname,x.CreatedDate
                ,Places =x.UserPlaces.Select
                (c=>c.Place.Name)
                }).FirstOrDefaultAsync();

            return Ok(new { 
            Message= "User place updated!",
            Place= result
            });

        }




        [HttpGet("users")]
        public async Task<IActionResult> GetUserPlace() {
        var user = await _context.Users
        .Include (x => x.UserPlaces)
        .ThenInclude (x => x.Place)
        .Select(x=> new {
        x.Id, x.Firstname, x.Lastname,
        Places=x.UserPlaces.Select(c=>c.Place.Name)
        }).ToListAsync();
            return Ok(new
            {
                Data = user
            });
        }





        // ==========================================
        // GENERATE JWT
        // ==========================================

        private string GenerateToken(User user)
        {
            // Claims stored inside JWT
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


            // Get JWT secret key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );


            // Signing credentials
            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );


            // Create JWT
            var token = new JwtSecurityToken(

                // Who created token
                issuer:
                    _configuration["Jwt:Issuer"],

                // Who can use token
                audience:
                    _configuration["Jwt:Audience"],

                // User information
                claims: claims,

                // Token expires after 1 hour
                expires:
                    DateTime.UtcNow.AddHours(1),

                // Sign token
                signingCredentials:
                    credentials
            );


            // Convert JWT object to string
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

    }
}
