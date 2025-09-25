using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Entity
{
    public class Movimiento
    {
        [Key]
        public int MovimientoId { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal? Saldo { get; set; }

        // Relación con Cuenta
        public Cuenta Cuenta { get; set; } = null!;
    }
}
