using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Domain.Entity
{
    public class Cuenta
    {
        [Key]
        public string NumeroCuenta { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string TipoCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
