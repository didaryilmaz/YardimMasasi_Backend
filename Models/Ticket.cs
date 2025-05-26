using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YardimMasasi.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string? Description { get; set; }
        public DateTime dateTime { get; set; }
        public bool IsCompleted { get; set; }

        // Talep oluşturan kullanıcı
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Tickets")]
        public User? User { get; set; }

        // Destek olarak atanan kullanıcı
        public int? AssignedSupportId { get; set; }

        [ForeignKey("AssignedSupportId")]
        [InverseProperty("AssignedTickets")]
        public User? AssignedSupport { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int PriorityId { get; set; }
        public Priority? Priority { get; set; }

        public ICollection<TicketResponse> TicketResponses { get; set; } = new List<TicketResponse>();
    }
}
