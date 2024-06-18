namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosDocumentosOpUpdateDto
    {
        public int CodigoImpuestoDocumentoOp { get; set; }
        public int CodigoDocumentoOp { get; set; }
        public int CodigoRetencion { get; set; }
        public int TipoRetencionId { get; set; }
        public int TipoImpuestoId { get; set; }
        public string PeriodoImpositivo { get; set; } = string.Empty;
        public int BaseImponible { get; set; }
        public int MontoImpuesto { get; set; }
        public int MontoImpuestoExento { get; set; }
        public int MontoRetenido { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
