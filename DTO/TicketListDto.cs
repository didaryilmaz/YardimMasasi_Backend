public class TicketListDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = "Açık";
    public string Category { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}
