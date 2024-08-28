namespace Convertidor.Dtos.Adm
{
    public class AdmOrdenPagoResponseDto
    {

        public int CodigoOrdenPago { get; set; }
        public int? ANO { get; set; }
        public int? CodigoCompromiso { get; set; }
        public int? CodigoOrdenCompra { get; set; }
        public int? CodigoContrato { get; set; }
        public int CodigoProveedor { get; set; }
        public string NumeroOrdenPago { get; set; } = string.Empty;
        public string ReferenciaOrdenPago { get; set; } = string.Empty;
        public DateTime FechaOrdenPago { get; set; }
        public string? FechaOrdenPagoString { get; set; }= string.Empty;
        public FechaDto? FechaOrdenPagoObj { get; set; }
        public int TipoOrdenPagoId { get; set; }
        public string DescripcionTipoOrdenPago { get; set; }= string.Empty;
        public DateTime FechaPlazoDesde { get; set; }
        public string? FechaPlazoDesdeString { get; set; }
        public FechaDto? FechaPlazoDesdeObj { get; set; }
        public DateTime? FechaPlazoHasta { get; set; }
        public string? FechaPlazoHastaString { get; set; }
        public FechaDto? FechaPlazoHastaObj { get; set; }
        public int? CantidadPago { get; set; }
        public int? NumeroPago { get; set; }
        public int? FrecuenciaPagoId { get; set; }
        
        public string DescripcionFrecuencia { get; set; }= string.Empty;
        public int? TipoPagoId { get; set; }
        public string DescripcionTipoPago { get; set; }= string.Empty;
        public int? NumeroValuacion { get; set; }
        public string? Status { get; set; } = string.Empty;
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

