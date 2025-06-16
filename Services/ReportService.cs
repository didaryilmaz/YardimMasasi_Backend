using Npgsql;
using System.Data;
using YardimMasasi_Backend.Services;

public class ReportService : IReportService
{
    private readonly string _connectionString;

    public ReportService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<object>> GetCategoryFrequencyAsync()
    {
        var list = new List<object>();

        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand("SELECT * FROM get_category_frequency()");
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                CategoryName = reader.GetString(0),
                TicketCount = reader.GetInt32(1)
            });
        }

        return list;
    }

    public async Task<IEnumerable<object>> GetPriorityFrequencyAsync()
    {
        var list = new List<object>();

        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand("SELECT * FROM get_priority_frequency()");
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                PriorityName = reader.GetString(0),
                TicketCount = reader.GetInt32(1)
            });
        }

        return list;
    }
}
