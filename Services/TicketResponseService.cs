using Microsoft.EntityFrameworkCore;
using YardimMasasi.Models;
using YardimMasasi_Backend.Services;

namespace YardimMasasi.Services
{
    public class TicketResponseService : ITicketResponseService
    {
        private readonly YardimMasasiDbContext _context;

        public TicketResponseService(YardimMasasiDbContext context)
        {
            _context = context;
        }

        public async Task<TicketResponse> AddResponseAsync(int ticketId, int userId, string responseText)
        {
            var response = new TicketResponse
            {
                TicketId = ticketId,
                UserId = userId,
                Response = responseText,
                DateTime = DateTime.UtcNow
            };

            _context.TicketResponses.Add(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<IEnumerable<TicketResponseDto>> GetResponsesAsync(int ticketId)
        {
            var responses = await _context.TicketResponses
                .Include(r => r.User)
                .Where(r => r.TicketId == ticketId)
                .OrderBy(r => r.DateTime)
                .Select(r => new TicketResponseDto
                {
                    TicketResponseId = r.TicketResponseId,
                    Response = r.Response,
                    CreatedAt = r.DateTime,
                    UserName = r.User != null ? r.User.Name : "Bilinmeyen Kullanıcı"
                })

                .ToListAsync();

            return responses;
        }
    }
}
