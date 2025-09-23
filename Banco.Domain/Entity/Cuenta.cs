using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Entity
{
    public class Cuenta
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = string.Empty;

        // Relación 1:N con Movimientos
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}
