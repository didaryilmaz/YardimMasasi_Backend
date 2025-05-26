using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Kullanıcının oluşturduğu ticketlar (talep sahibi)
        [InverseProperty("User")]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Kullanıcının destek olarak atandığı ticketlar
        [InverseProperty("AssignedSupport")]
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();

        public ICollection<SupportCategory> SupportCategories { get; set; } = new List<SupportCategory>();

        public User() { }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
