using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardimMasasi.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public int PriorityLevel { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}