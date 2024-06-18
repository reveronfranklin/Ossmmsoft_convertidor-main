namespace Convertidor.Dtos.Adm
{
    public class AdmPeriodicoOpUpdateDto
    {
        public int CodigoPeriodicoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CantidadPago { get; set; }
        public string NumeroPago { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
