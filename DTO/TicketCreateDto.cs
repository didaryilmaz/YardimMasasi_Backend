using System.ComponentModel.DataAnnotations;

public class TicketCreateDto
{
    [Required]
    public string? Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int PriorityId { get; set; }
}
