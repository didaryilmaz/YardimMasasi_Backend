using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace YardimMasasi
{
    public class YardimMasasiDbContext : DbContext
    {
        public YardimMasasiDbContext(DbContextOptions<YardimMasasiDbContext> options)
            : base(options)
        {
        }
        public YardimMasasiDbContext(){}
        
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Ticket> Tickets { get; set; }
        public DbSet<Models.TicketResponse> TicketResponses { get; set; }
        public DbSet<Models.Priority> Priorities { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=YardimMasasiDb;Username=postgres;Password=ddrylmz");
            }
        }
    }
}