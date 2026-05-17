using Microsoft.EntityFrameworkCore;
using SistemGestionareFinantePersonale1.Models;

namespace SistemGestionareFinantePersonale1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");
                entity.Property(t => t.Description).HasMaxLength(500);
                entity.Property(t => t.Type).HasMaxLength(50);

                // Persist only the category name (Category is a flyweight object in-memory)
                entity.Ignore(t => t.Category);
                entity.Property<string>("CategoryName").HasMaxLength(100);
            });
        }
    }
}
