namespace Convertidor.Dtos.Adm
{
    public class AdmDocumentosOpUpdateDto
    {
        public int CodigoDocumentoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public DateTime? FechaComprobante { get; set; }
        public string PeriodoImpositivo { get; set; } = string.Empty;
        public int TipoOperacionId { get; set; }
        public int TipoDocumentoId { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NumeroControlDocumento { get; set; } = string.Empty;
        public decimal MontoDocumento { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal MontoImpuesto { get; set; }
        public string NumeroDocumentoAfectado { get; set; } = string.Empty;
        public int TipoTransaccionId { get; set; }
        public int TipoImpuestoId { get; set; }
        public decimal MontoImpuestoExento { get; set; }
        public decimal MontoRetenido { get; set; }
    
        public int CodigoPresupuesto { get; set; }
        public string NumeroExpediente { get; set; } = string.Empty;
        public int EstatusFiscoId { get; set; }
    }
}
