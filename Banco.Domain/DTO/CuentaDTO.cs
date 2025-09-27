using Banco.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Banco.Domain.DTO
{
    public class CuentaDTO
    {
        [Key]
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = string.Empty; //1 activo y 0 inactivo
        public string Cliente { get; set; } = null!;

    }
}
