using Microsoft.EntityFrameworkCore;
using QuickHealth.Domain.Model;

namespace QuickHealth.Infrastructure.Adapters.Out.Persistence.Context
{
    public class QuickHealthDbContext:DbContext
    {
        public QuickHealthDbContext(DbContextOptions<QuickHealthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>().Property(p => p.Temperatura).HasColumnType("decimal(4,1)");
        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}
