using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            // Enable foreign key constraints for SQLite
            Database.OpenConnection();
            Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Explicitly configure EventParticipant relationships
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Team)
                .WithMany()
                .HasForeignKey(ep => ep.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Role)
                .WithMany()
                .HasForeignKey(ep => ep.RoleId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
