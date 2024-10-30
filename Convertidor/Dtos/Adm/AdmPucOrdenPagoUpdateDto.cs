namespace Convertidor.Dtos.Adm
{
    public class AdmPucOrdenPagoUpdateDto
    {
        public int CodigoPucOrdenPago { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoPucCompromiso { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoPuc { get; set; }
        public int FinanciadoId { get; set; }
        public int CodigoFinanciado { get; set; }
        public int CodigoSaldo { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoCompromisoOp { get; set; }
        public int CodigoPresupuesto { get; set; }
    }
}
