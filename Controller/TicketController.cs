using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        public TicketController(ITicketService ticketService, ITicketResponseService ticketResponseService)
        {
            _ticketService = ticketService;
            _ticketResponseService = ticketResponseService;
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
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var createdTicket = await _ticketService.CreateTicketAsync(dto, int.Parse(userId));
            return Ok(createdTicket);
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
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpGet("report/category-frequency")]
        public async Task<IActionResult> GetCategoryFrequency([FromServices] IReportService reportService)
        {
            var result = await reportService.GetCategoryFrequencyAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("report/priority-frequency")]
        public async Task<IActionResult> GetPriorityFrequency([FromServices] IReportService reportService)
        {
            var result = await reportService.GetPriorityFrequencyAsync();
            return Ok(result);
        }
    }
}
 