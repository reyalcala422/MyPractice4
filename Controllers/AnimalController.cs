using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using MyPractice4.DTO.Animal;
using MyPractice4.Model;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AnimalController : ControllerBase
    {
        private readonly APIDbContext _context;
        public AnimalController(APIDbContext  context) {
        _context=context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(InserAnimalDTO dto) {


            var animal = new Animal {
            Name=dto.Name
            };

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            
            return Ok(new {
            Message="Animal Inserted!",
            Data=animal
            });
        }



        [HttpGet]
        public async Task<IActionResult> Get() {

            var animal = await _context.Animals
            .Select(x => new GetAnimalDTO { 
            Id=x.Id,
            Name=x.Name,
            CreatedDate=x.CreatedDate,
            }).ToListAsync();

            return Ok(animal);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAnimalDTO dto)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null) {
                return NotFound("Animal not found!");
            }
            var updateAnimal = new { 
            Name= dto.Name,
            };

            animal.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok(new { 
            Message="Animal Updated!",
            Data= updateAnimal
            });
        }

    }
}
