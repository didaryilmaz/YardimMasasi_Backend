using Npgsql;
using YardimMasasi.Models;
using Microsoft.Extensions.Configuration;

public class TicketService : ITicketService
{
    private readonly string _connectionString;

    public TicketService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<bool> CreateTicketAsync(TicketCreateDto dto)
    {
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("CALL create_ticket(@description, @categoryId, @priorityId, @userId)");

            cmd.Parameters.AddWithValue("@description", dto.Description);
            cmd.Parameters.AddWithValue("@categoryId", dto.CategoryId);
            cmd.Parameters.AddWithValue("@priorityId", dto.PriorityId);
            cmd.Parameters.AddWithValue("@userId", dto.UserId);
            
            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            return false;
        }
        
    }

    public async Task<bool> UpdateTicketAsync(int ticketId, TicketUpdateDto dto)
    {
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("CALL update_ticket($1, $2, $3, $4, $5)");

            cmd.Parameters.AddWithValue(ticketId);
            cmd.Parameters.AddWithValue(dto.Description);
            cmd.Parameters.AddWithValue(dto.CategoryId);
            cmd.Parameters.AddWithValue(dto.PriorityId);
            cmd.Parameters.AddWithValue(dto.IsCompleted);

            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTicketAsync(int ticketId)
    {
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("CALL delete_ticket($1)");

            cmd.Parameters.AddWithValue(ticketId);
            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            return false;
        }
    }

    public async Task<List<TicketListDto>> GetTicketsForSupportUserAsync(int supportUserId)
    {
        var result = new List<TicketListDto>();

        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("SELECT * FROM get_tickets_for_support($1)");
            cmd.Parameters.AddWithValue(supportUserId);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new TicketListDto
                {
                    Id = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Category = reader.GetString(2),
                    Priority = reader.GetString(3),
                    Status = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5),
                    UserId = reader.GetInt32(6),
                    AssignedSupportId = reader.IsDBNull(7) ? null : reader.GetInt32(7)
                });
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
        }

        return result;
    }

    public async Task<List<TicketListDto>> GetAllTicketsAsync()
    {
        var result = new List<TicketListDto>();

        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("SELECT * FROM get_all_tickets()");

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new TicketListDto
                {
                    Id = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Category = reader.GetString(2),
                    Priority = reader.GetString(3),
                    Status = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5),
                    UserId = reader.GetInt32(6),
                    AssignedSupportId = reader.IsDBNull(7) ? null : reader.GetInt32(7)
                });
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
        }

        return result;
    }

    public async Task<TicketListDto?> GetTicketByIdAsync(int ticketId)
    {
        try
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("SELECT * FROM get_ticket_by_id($1)");
            cmd.Parameters.AddWithValue(ticketId);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TicketListDto
                {
                    Id = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Category = reader.GetString(2),
                    Priority = reader.GetString(3),
                    Status = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5),
                    UserId = reader.GetInt32(6),
                    AssignedSupportId = reader.IsDBNull(7) ? null : reader.GetInt32(7)
                };
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Veritabanı hatası: {ex.Message}");
        }

        return null;
    }
}
