namespace Convertidor.Dtos.Cnt
{
    public class CntBancoArchivoControlResponseDto
    {
        public int CodigoBancoArchivoControl { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoCuentaBanco { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
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
        public int CodigoEstadoCuenta { get; set; }
    }
}
