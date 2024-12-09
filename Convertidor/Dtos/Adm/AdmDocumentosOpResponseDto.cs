namespace Convertidor.Dtos.Adm
{
    public class AdmDocumentosOpResponseDto
    {
        public int CodigoDocumentoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public DateTime FechaComprobante { get; set; }
        public string FechaComprobanteString { get; set; } 
        public FechaDto FechaComprobanteObj { get; set; }
        public string PeriodoImpositivo { get; set; } = string.Empty;
        public int TipoOperacionId { get; set; }
        public string DescripcionTipoOperacion { get; set; } = string.Empty;
        public int TipoDocumentoId { get; set; }
        public string DescripcionTipoDocumento{ get; set; } = string.Empty;
        public int TipoTransaccionId { get; set; }
        public string DescripcionTipoTransaccion { get; set; } = string.Empty;
        public int TipoImpuestoId { get; set; }
        public string DescripcionTipoImpuesto { get; set; } = string.Empty;
        public int? EstatusFiscoId { get; set; }
        public string DescripcionEstatusFisco { get; set; } = string.Empty;
        public DateTime FechaDocumento { get; set; }
        public string FechaDocumentoString { get; set; }
        public FechaDto FechaDocumentoObj { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NumeroControlDocumento { get; set; } = string.Empty;
        public decimal MontoDocumento { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal MontoImpuesto { get; set; }
        public string? NumeroDocumentoAfectado { get; set; } = string.Empty;

        public decimal MontoImpuestoExento { get; set; }
        public decimal MontoRetenido { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string? NumeroExpediente { get; set; } = string.Empty;
        
    }
}
