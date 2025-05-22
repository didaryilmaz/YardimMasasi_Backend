using Microsoft.EntityFrameworkCore;
using YardimMasasi;
using YardimMasasi.Models;

public class TicketService : ITicketService
{
    private readonly YardimMasasiDbContext _context;

    public TicketService(YardimMasasiDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketListDto>> GetAllTicketsAsync()
    {
        return await _context.Tickets
            .Include(t => t.Category)
            .Include(t => t.Priority)
            .Select(t => new TicketListDto
            {
                Id = t.TicketId,
                Description = t.Description,
                Category = t.Category.CategoryName,
                Priority = t.Priority.PriorityName,
                Status = t.IsCompleted ? "Tamamlandı" : "Açık",
                CreatedAt = t.dateTime,
                UserId = t.UserId
            }).ToListAsync();
    }

    public async Task<TicketListDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Category)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (ticket == null)
            return null;

        return new TicketListDto
        {
            Id = ticket.TicketId,
            Description = ticket.Description,
            Category = ticket.Category?.CategoryName ?? "",
            Priority = ticket.Priority?.PriorityName ?? "",
            Status = ticket.IsCompleted ? "Tamamlandı" : "Açık",
            CreatedAt = ticket.dateTime,
            UserId = ticket.UserId
        };
    }

    public async Task<bool> UpdateTicketAsync(int id, TicketUpdateDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return false;

        ticket.Description = dto.Description;
        ticket.CategoryId = dto.CategoryId;
        ticket.PriorityId = dto.PriorityId;
        ticket.IsCompleted = dto.IsCompleted;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Ticket> CreateTicketAsync(TicketCreateDto dto, int userId)
    {
        var ticket = new Ticket
        {
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            PriorityId = dto.PriorityId,
            dateTime = DateTime.UtcNow,
            UserId = userId
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return ticket;
    }

}
