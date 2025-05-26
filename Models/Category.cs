using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardimMasasi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<SupportCategory> SupportCategories { get; set; } = new List<SupportCategory>();

    }
}