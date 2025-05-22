using System.Collections.Generic;
using System.Threading.Tasks;
using YardimMasasi.Models;

namespace YardimMasasi_Backend.Services
{
    public interface ITicketResponseService
    {
        Task<IEnumerable<TicketResponseDto>> GetResponsesAsync(int ticketId);
        Task<TicketResponse> AddResponseAsync(int ticketId, int userId, string responseText);
    }
}
