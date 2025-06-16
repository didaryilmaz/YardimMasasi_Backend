public interface ITicketService
{
    Task<bool> CreateTicketAsync(TicketCreateDto dto);
    Task<bool> UpdateTicketAsync(int ticketId, TicketUpdateDto dto);
    Task<bool> DeleteTicketAsync(int ticketId);
    Task<List<TicketListDto>> GetTicketsForSupportUserAsync(int supportUserId);
    Task<List<TicketListDto>> GetAllTicketsAsync();
    Task<TicketListDto?> GetTicketByIdAsync(int ticketId);
}
