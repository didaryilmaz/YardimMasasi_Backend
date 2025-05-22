using YardimMasasi.Models;

public interface ITicketService
{
    Task<List<TicketListDto>> GetAllTicketsAsync();
    Task<TicketListDto?> GetTicketByIdAsync(int id);
    Task<bool> UpdateTicketAsync(int id, TicketUpdateDto dto);
    Task<bool> DeleteTicketAsync(int id);
    Task<Ticket> CreateTicketAsync(TicketCreateDto dto, int userId);
}
