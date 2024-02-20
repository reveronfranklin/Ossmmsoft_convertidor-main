namespace Convertidor.Dtos.Adm
{
    public class AdmDocumentosOpUpdateDto
    {
        public int CodigoDocumentoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public DateTime FechaComprobante { get; set; }
        public string PeriodoImpositivo { get; set; } = string.Empty;
        public int TipoOperacionId { get; set; }
        public int TipoDocumentoId { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NumeroControlDocumento { get; set; } = string.Empty;
        public int MontoDocumento { get; set; }
        public int BaseImponible { get; set; }
        public int MontoImpuesto { get; set; }
        public string NumeroDocumentoAfectado { get; set; } = string.Empty;
        public int TipoTransaccionId { get; set; }
        public int TipoImpuestoId { get; set; }
        public int MontoImpuestoExento { get; set; }
        public int MontoRetenido { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
        public string NumeroExpediente { get; set; } = string.Empty;
        public int EstatusFiscoId { get; set; }
    }
}
