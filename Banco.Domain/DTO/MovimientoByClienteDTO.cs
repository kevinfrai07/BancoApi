namespace Banco.Domain.DTO
{
    public class MovimientoListadoDTO
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal Movimiento { get; set; } // ya sea positivo o negativo
        public decimal SaldoDisponible { get; set; }
    }
}
