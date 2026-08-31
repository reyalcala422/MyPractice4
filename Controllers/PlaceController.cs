using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using MyPractice4.DTO.Place;
using MyPractice4.Model;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly APIDbContext _context;

        public PlaceController(APIDbContext context) {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post(InsertPlace dto) {
            var place = new Place
            {
                Name = dto.Name
            };
            _context.Places.Add(place);
            await _context.SaveChangesAsync();
            return Ok(new {
            Message="Place Inserted",
            data= place
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var place = await _context.Places
            .Select(x=> new GetPlaceDTO {
            Id= x.Id,
            Name= x.Name,
            CreatedDate=x.CreatedDate
            }).ToListAsync();
            return Ok(place);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePlaceDTO dto) {
            var place = await _context.Places.FindAsync(id);
            if (place==null) {
                return NotFound("Place not found!");
            }

            var updatedPlace = new
            {
                Name = dto.Name,
            };
            place.Name= dto.Name;
            await _context.SaveChangesAsync();
            return Ok(new {
            Message = "Place updated",
            Data= updatedPlace
            });

        }
    }
}
