using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using System.Security.Claims;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {


        private readonly APIDbContext _context;
        public TaskController(APIDbContext context) { 
        _context=context;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks() {
            int userId = GetUserId();

            var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .Select(x=> new { 
            x.Id,x.Title,x.Description,x.UserId
            }).ToListAsync();
            return Ok(tasks);
        }




        private int GetUserId()
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException(
                    "User ID was not found in the JWT token."
                );
            }

            return int.Parse(userId);
        }
    }
}
