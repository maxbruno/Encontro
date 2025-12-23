using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Encontro.Infrastructure
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
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

            // Índices para otimizar buscas
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Name);
            
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Email);
            
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name);

            // Global query filter for soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(GetSoftDeleteFilter(entityType.ClrType));
                }
            }
        }

        private static LambdaExpression GetSoftDeleteFilter(Type entityType)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(entityType, "e");
            var property = System.Linq.Expressions.Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            var condition = System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false));
            return System.Linq.Expressions.Expression.Lambda(condition, parameter);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteProperties()
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    // DeletedBy pode ser preenchido por um serviço que tenha acesso ao usuário atual
                }
            }
        }
    }
}
