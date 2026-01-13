namespace Convertidor.Dtos.Cnt
{
    public class CntEstadoCuentasUpdateDto
    {
        public int CodigoEstadoCuenta { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public string NumeroEstadoCuenta { get; set; } = string.Empty;
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
