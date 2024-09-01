namespace Convertidor.Dtos.Adm
{
    public class AdmOrdenPagoResponseDto
    {

        public int CodigoOrdenPago { get; set; }
        public int? ANO { get; set; }

        public string CodigoCompromiso { get; set; } = string.Empty;
        
        public int CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string NumeroOrdenPago { get; set; } = string.Empty;
        public DateTime FechaOrdenPago { get; set; }
        public string? FechaOrdenPagoString { get; set; }= string.Empty;
        public FechaDto? FechaOrdenPagoObj { get; set; }
        public int TipoOrdenPagoId { get; set; }
        public string DescripcionTipoOrdenPago { get; set; }= string.Empty;
        public int? CantidadPago { get; set; }
        public int? FrecuenciaPagoId { get; set; }
        
        public string DescripcionFrecuencia { get; set; }= string.Empty;
        public int? TipoPagoId { get; set; }
        public string DescripcionTipoPago { get; set; }= string.Empty;
        public string? Status { get; set; } = string.Empty;
        public string DescripcionStatus { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        
        public int CodigoPresupuesto { get; set; }
        public decimal? NumeroComprobante { get; set; }
        public DateTime FechaComprobante { get; set; }
        public string FechaComprobanteString { get; set; }
        public FechaDto FechaComprobanteObj { get; set; }
        public decimal? NumeroComprobante2 { get; set; }
        public decimal? numeroComprobante3 { get; set; }
        public decimal? NumeroComprobante4 { get; set; }
    }
}

