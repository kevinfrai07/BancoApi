using Banco.Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace Banco.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<Cuenta>().ToTable("cuenta");
            modelBuilder.Entity<Movimiento>().ToTable("movimientos");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {



        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
