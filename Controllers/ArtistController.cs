using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPractice4.Data;
using MyPractice4.DTO.Artist;
using MyPractice4.Model;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ArtistController : ControllerBase
    {

        private readonly APIDbContext _context;

        public ArtistController(APIDbContext context) {
            _context  = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post(InsertArtistDTO dto) {
            var artist = new Artist
            {
                FullName = dto.FullName,
                Talent = dto.Talent,
            };
            
            _context .Artists.Add(artist);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Artist Inserted!",
                Data = artist
            });
        }
    }
}
