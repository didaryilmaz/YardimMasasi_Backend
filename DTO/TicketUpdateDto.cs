using System.ComponentModel.DataAnnotations;

public class TicketUpdateDto
{
    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int PriorityId { get; set; }

    public bool IsCompleted { get; set; }
}
