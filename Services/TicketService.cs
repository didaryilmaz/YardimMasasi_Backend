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
                UserId = t.UserId,
                AssignedSupportId = t.AssignedSupportId
            })
            .ToListAsync();
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
        Console.WriteLine($"Ticket oluşturuluyor. CategoryId: {dto.CategoryId}");

        var ticket = new Ticket
        {
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            PriorityId = dto.PriorityId,
            dateTime = DateTime.UtcNow,
            UserId = userId,
            IsCompleted = false
        };

        var availableSupportUsers = await _context.SupportCategories
            .Where(sc => sc.CategoryId == dto.CategoryId && sc.User != null && sc.User.Role == "Destek")
            .Include(sc => sc.User)
            .Select(sc => sc.User!)
            .ToListAsync();

        Console.WriteLine($"Kategoriye atanmış destek sayısı: {availableSupportUsers.Count}");

        if (availableSupportUsers.Any())
        {
            // Her destek kullanıcısı için aktif ticket sayısını al
            var supportWithTicketCounts = new List<(User user, int activeTicketCount)>();

            foreach (var user in availableSupportUsers)
            {
                var count = await _context.Tickets
                    .CountAsync(t => t.AssignedSupportId == user.UserId && !t.IsCompleted);

                supportWithTicketCounts.Add((user, count));
            }

            // En az aktif ticket'a sahip kullanıcıyı bul
            var selectedSupport = supportWithTicketCounts
                .OrderBy(x => x.activeTicketCount)
                .First();

            ticket.AssignedSupportId = selectedSupport.user.UserId;
            Console.WriteLine($"Destek kullanıcısı atandı: {ticket.AssignedSupportId}");
        }
        else
        {
            Console.WriteLine("Bu kategoriye atanmış destek kullanıcısı bulunamadı.");
        }

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        Console.WriteLine($"Kaydedilen ticket AssignedSupportId: {ticket.AssignedSupportId}");

        return ticket;
    }
    
    public async Task<List<TicketListDto>> GetTicketsForSupportUserAsync(int supportUserId)
    {
        var filteredTickets = await _context.Tickets
            .Where(t => t.AssignedSupportId == supportUserId && !t.IsCompleted)
            .OrderBy(t => t.dateTime)
            .Select(t => new TicketListDto
            {
                Id = t.TicketId,
                Description = t.Description,
                Category = t.Category.CategoryName,
                Priority = t.Priority.PriorityName,
                Status = t.IsCompleted ? "Tamamlandı" : "Açık",
                CreatedAt = t.dateTime,
                UserId = t.UserId,
                AssignedSupportId = t.AssignedSupportId
            })
            .ToListAsync();

        return filteredTickets;
    }



}