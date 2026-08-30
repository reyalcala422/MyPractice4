using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
