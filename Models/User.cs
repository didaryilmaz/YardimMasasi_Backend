using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YardimMasasi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; } 
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = string.Empty; 
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
    }
}