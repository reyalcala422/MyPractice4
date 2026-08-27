using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPractice4.Data;
using MyPractice4.DTO;
using MyPractice4.Model;
using System.Security.Claims;

namespace MyPractice4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {


        private readonly APIDbContext _context;
        public TaskController(APIDbContext context)
        {
            _context = context;
        }
        // ==========================================
        // GET ALL TASKS OF LOGGED-IN USER
        // GET: api/task
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            int userId = GetUserId();

            var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Description,
                x.UserId
            }).ToListAsync();
            return Ok(tasks);
        }

        // ==========================================
        // GET ONE TASK
        // GET: api/task/1
        // ==========================================



        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            int userId = GetUserId();
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound("Task not found");

            }
            return Ok(task);
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

        // ==========================================
        // CREATE TASK
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Post(InsertTaskDTO dto)
        {
            int userId = GetUserId();
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                // User from JWT
                UserId = userId
            };
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Task created successfully",
                datails = task
            });
        }
        // ==========================================
        // UPDATE TASK
        // ==========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDTO dto)
        {
            int userId = GetUserId();
            var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
            {
                return NotFound("Task not found.");
            }
            task.Title = dto.Title;
            task.Description = dto.Description;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Task updated successfully",
                details = task
            });
        }
    }
}
