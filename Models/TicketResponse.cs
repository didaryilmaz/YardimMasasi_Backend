using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardimMasasi.Models
{
    public class TicketResponse
    {
        public int TicketResponseId { get; set; }
        public string? Response { get; set; }
        public DateTime dateTime { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}