using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YardimMasasi.Models;

namespace YardimMasasi
{
    public class YardimMasasiDbContext : DbContext
    {
        public YardimMasasiDbContext(DbContextOptions<YardimMasasiDbContext> options)
            : base(options)
        {
        }
        public YardimMasasiDbContext() { }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Ticket> Tickets { get; set; }
        public DbSet<Models.TicketResponse> TicketResponses { get; set; }
        public DbSet<Models.Priority> Priorities { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.SupportCategory> SupportCategories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=YardimMasasiDb;Username=postgres;Password=ddrylmz");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ticket -> User (talep oluşturan)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> AssignedSupport (destek personeli)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedSupport)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedSupportId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
