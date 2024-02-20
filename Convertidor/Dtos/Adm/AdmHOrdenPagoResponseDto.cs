namespace Convertidor.Dtos.Adm
{
    public class AdmHOrdenPagoResponseDto
    {
        public int CodigoHOrdenPago { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int? ANO { get; set; }
        public int? CodigoCompromiso { get; set; }
        public int? CodigoOrdenCompra { get; set; }
        public int? CodigoContrato { get; set; }
        public int CodigoProveedor { get; set; }
        public string NumeroOrdenPago { get; set; } = string.Empty;
        public string ReferenciaOrdenPago { get; set; } = string.Empty;
        public DateTime FechaOrdenPago { get; set; }
        public string FechaOrdenPagoString { get; set; }
        public FechaDto FechaOrdenPagoObj { get; set; }
        public int TipoOrdenPagoId { get; set; }
        public DateTime? FechaPlazoDesde { get; set; }
        public string FechaPlazoDesdeString { get; set; }
        public FechaDto FechaPlazoDesdeObj { get; set; }
        public DateTime? FechaPlazoHasta { get; set; }
        public string FechaPlazoHastaString { get; set; }
        public FechaDto FechaPlazoHastaObj { get; set; }
        public int? CantidadPago { get; set; }
        public int? NumeroPago { get; set; }
        public int? FrecuenciaPagoId { get; set; }
        public int? TipoPagoId { get; set; }
        public int? NumeroValuacion { get; set; }
        public string? Status { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string? Extra1 { get; set; } = string.Empty;
        public string? Extra2 { get; set; } = string.Empty;
        public string? Extra3 { get; set; } = string.Empty;
        public int? UsuarioHIns { get; set; }
        public DateTime FechaHIns { get; set; }
        public string FechaHInsString { get; set; }
        public FechaDto FechaHInsObj { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string? Extra4 { get; set; } = string.Empty;
        public string? Extra5 { get; set; } = string.Empty;
        public string? Extra6 { get; set; } = string.Empty;
        public string? Extra7 { get; set; } = string.Empty;
        public string? Extra8 { get; set; } = string.Empty;
        public string? Extra9 { get; set; } = string.Empty;
        public string? Extra10 { get; set; } = string.Empty;
        public string? Extra11 { get; set; } = string.Empty;
        public string? Extra12 { get; set; } = string.Empty;
        public string? Extra13 { get; set; } = string.Empty;
        public string? Extra14 { get; set; } = string.Empty;
        public string? Extra15 { get; set; } = string.Empty;
        public int? NumeroComprobante { get; set; }
        public DateTime? FechaComprobante { get; set; }
        public string FechaComprobanteString { get; set; }
        public FechaDto FechaComprobanteObj { get; set; }
       
    }
}

