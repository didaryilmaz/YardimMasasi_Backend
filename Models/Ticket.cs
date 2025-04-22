using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardimMasasi.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string? Description { get; set; }
        public DateTime dateTime { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int PriorityId { get; set; }
        public Priority? Priority { get; set; }
        public TicketResponse? TicketResponses { get; set; } 
    }
}