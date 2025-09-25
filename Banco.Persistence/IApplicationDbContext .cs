using Banco.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Banco.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Persona> Personas { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Cuenta> Cuentas { get; set; }
        DbSet<Movimiento> Movimientos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
