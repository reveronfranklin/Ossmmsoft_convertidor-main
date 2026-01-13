namespace Convertidor.Dtos.Cnt
{
    public class CntEstadoCuentasResponseDto
    {
        public int CodigoEstadoCuenta { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public string NumeroEstadoCuenta { get; set; } = string.Empty;
        public DateTime FechaDesde { get; set; }
        public string FechaDesdeString { get; set; }
        public FechaDto FechaDesdeObj { get; set; }
        public DateTime FechaHasta { get; set; }
        public string FechaHastaString { get; set; }
        public FechaDto FechaHastaObj { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
