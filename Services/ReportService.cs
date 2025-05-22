using Microsoft.EntityFrameworkCore;
using YardimMasasi;
using YardimMasasi_Backend.Services;

public class ReportService : IReportService
{
    private readonly YardimMasasiDbContext _context;

    public ReportService(YardimMasasiDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetCategoryFrequencyAsync()
    {
        var result = await _context.Categories
            .Select(c => new
            {
                c.CategoryName,
                TicketCount = c.Tickets.Count
            })
            .ToListAsync();

        return result;
    }

    public async Task<object> GetPriorityFrequencyAsync()
    {
        var result = await _context.Priorities
            .Select(p => new
            {
                p.PriorityName,
                TicketCount = p.Tickets.Count
            })
            .ToListAsync();

        return result;
    }

}
