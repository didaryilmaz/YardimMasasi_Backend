using Microsoft.EntityFrameworkCore;
using Npgsql;
using YardimMasasi.Models;
using YardimMasasi_Backend.Services;

namespace YardimMasasi.Services
{
    public class TicketResponseService : ITicketResponseService
    {
        private readonly string _connectionString;

        public TicketResponseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<TicketResponse> AddResponseAsync(int ticketId, int userId, string responseText)
        {
            await using var dataSource = NpgsqlDataSource.Create(_connectionString);
            await using var cmd = dataSource.CreateCommand("SELECT * FROM add_ticket_response($1, $2, $3)");
            cmd.Parameters.AddWithValue(ticketId);
            cmd.Parameters.AddWithValue(userId);
            cmd.Parameters.AddWithValue(responseText);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TicketResponse
                {
                    TicketResponseId = reader.GetInt32(0),
                    Response = reader.GetString(1),
                    DateTime = reader.GetDateTime(2),
                    TicketId = reader.GetInt32(3),
                    UserId = reader.GetInt32(4)
                };
            }


            throw new Exception("Yanıt eklenemedi.");
        }

        public async Task<IEnumerable<TicketResponseDto>> GetResponsesAsync(int ticketId)
        {
            var responses = new List<TicketResponseDto>();

            try
            {
                await using var dataSource = NpgsqlDataSource.Create(_connectionString);
                await using var cmd = dataSource.CreateCommand("SELECT * FROM get_ticket_responses($1)");
                cmd.Parameters.AddWithValue(ticketId);

                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    responses.Add(new TicketResponseDto
                    {
                        TicketResponseId = reader.GetInt32(0),
                        Response = reader.GetString(1),
                        CreatedAt = reader.GetDateTime(2),
                        UserName = reader.GetString(3)
                    });
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Veritabanı hatası: {ex.Message}");
                // Log hatası alınabilir.
            }

            return responses;
        }

    }
}
