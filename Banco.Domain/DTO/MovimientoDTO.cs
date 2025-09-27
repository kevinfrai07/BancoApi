namespace Banco.Domain.DTO
{
    public class MovimientoDTO
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Movimiento { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
    }
}
