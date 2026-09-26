using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using MyPractice4.DTO.Genre;
using MyPractice4.Model;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly APIDbContext _context;


        public GenreController(APIDbContext context) {
        _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertGenreDTO dto) {

            var genre = new Genre
            {
                Name = dto.Name,
            };

            _context .Genres.Add(genre); 
            await _context.SaveChangesAsync();

            return Ok(new {
            Message="Genre Inserted",
            Data=genre
            });
        }


        [HttpGet]
        public async Task<IActionResult> Get() {
            var artist = await _context.Genres
            .Select(x => new
            {
                
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

           
            return Ok(artist);
         
        }



        [HttpPut("update/genre/{id}")]
        public async Task<IActionResult> Put(int id, UpdateGenreDTO dto) {

            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
                return NotFound("Genre not found!");

            var updateGenre = new
            {
                Name = dto.Name
            };

            genre.Name= dto.Name;

            await _context.SaveChangesAsync();

            return Ok(new { 
            Mesasge="Genre Updated!",
            Data=updateGenre
            });
        }

    }
}
