namespace Convertidor.Dtos.Adm
{
    public class AdmPeriodicoOpResponseDto
    {
        public int CodigoPeriodicoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CantidadPago { get; set; }
        public string NumeroPago { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public string FechaPagoString { get; set; }
        public FechaDto FechaPagoObj { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public float MontoAnulado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}
