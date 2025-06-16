using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YardimMasasi;
using YardimMasasi.Models;
using YardimMasasi_Backend.Services;

namespace YardimMasasi_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowReactApp")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketResponseService _ticketResponseService;
        private readonly YardimMasasiDbContext _context;
        private readonly IReportService _reportService;

        public TicketController(
            ITicketService ticketService,
            ITicketResponseService ticketResponseService,
            IReportService reportService,
            YardimMasasiDbContext context
            )
        {
            _ticketService = ticketService;
            _ticketResponseService = ticketResponseService;
            _reportService = reportService;
            _context = context;
        }

        [HttpGet("mytickets")]
        public async Task<ActionResult<IEnumerable<TicketListDto>>> GetAllTickets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("User not found.");

            int userId = int.Parse(userIdClaim);
            var tickets = await _ticketService.GetAllTicketsAsync();
            var userTickets = tickets.Where(t => t.UserId == userId).ToList();

            return Ok(userTickets);
        }

        [HttpPost("createTicket")]
        [Authorize]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            dto.UserId = userId;

            var result = await _ticketService.CreateTicketAsync(dto);
            return Ok(result);
        }


        


        [HttpPut("updateTicket/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketUpdateDto dto)
        {
            var result = await _ticketService.UpdateTicketAsync(id, dto);
            if (!result)
                return NotFound("Ticket not found or update failed.");

            return NoContent();
        }

        [HttpDelete("deleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await _ticketService.DeleteTicketAsync(id);
            if (!result)
                return NotFound("Ticket not found.");

            return NoContent();
        }

        [Authorize(Roles = "Destek,Admin")]
        [HttpGet("getAllTicketsByRole")]
        public async Task<IActionResult> GetAllTicketsByRole()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "Admin")
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return Ok(tickets);
            }

            if (userRole == "Destek")
            {
                var tickets = await _ticketService.GetTicketsForSupportUserAsync(userId);
                return Ok(tickets);
            }

            return Forbid();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [Authorize(Roles = "User,Destek")]
        [HttpGet("{ticketId}/responses")]
        public async Task<IActionResult> GetResponses(int ticketId)
        {
            var responses = await _ticketResponseService.GetResponsesAsync(ticketId);
            return Ok(responses);
        }

        [HttpPost("{ticketId}/responses")]
        public async Task<IActionResult> AddResponse(int ticketId, [FromBody] TicketResponseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Response))
                return BadRequest("Yanıt boş olamaz.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null)
                return Unauthorized();

            int userId = int.Parse(userIdStr);

            var response = await _ticketResponseService.AddResponseAsync(ticketId, userId, dto.Response);
            return Ok(response);
        }

        [Authorize(Roles = "Destek,Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTickets([FromQuery] string? category, [FromQuery] string? priority)
        {
            var allTickets = await _ticketService.GetAllTicketsAsync();

            if (!string.IsNullOrEmpty(category))
                allTickets = allTickets.Where(t => t.Category == category).ToList();

            if (!string.IsNullOrEmpty(priority))
                allTickets = allTickets.Where(t => t.Priority == priority).ToList();

            return Ok(allTickets);
        }

        [Authorize(Roles = "Destek,Admin")]
        [HttpGet("report/bycategory")]
        public async Task<IActionResult> GetTicketCountsByCategory()
        {
            var allTickets = await _ticketService.GetAllTicketsAsync();
            var grouped = allTickets
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList();

            return Ok(grouped);
        }

        [Authorize(Roles = "Destek,Admin")]
        [HttpGet("report/category-frequency")]
        public async Task<IActionResult> GetCategoryFrequency()
        {
            var result = await _reportService.GetCategoryFrequencyAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Destek,Admin")]
        [HttpGet("report/priority-frequency")]
        public async Task<IActionResult> GetPriorityFrequency()
        {
            var result = await _reportService.GetPriorityFrequencyAsync();
            return Ok(result);
        }
        
        // Kategori işlemleri
        [Authorize(Roles = "Admin,User")]
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                return BadRequest("Kategori adı boş olamaz.");

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Öncelik işlemleri
        [Authorize(Roles = "Admin,User")]
        [HttpGet("priorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = await _context.Priorities.ToListAsync();
            return Ok(priorities);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("priorities")]
        public async Task<IActionResult> AddPriority([FromBody] Priority priority)
        {
            if (string.IsNullOrWhiteSpace(priority.PriorityName))
                return BadRequest("Öncelik adı boş olamaz.");

            _context.Priorities.Add(priority);
            await _context.SaveChangesAsync();
            return Ok(priority);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("priorities/{id}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
                return NotFound();

            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}