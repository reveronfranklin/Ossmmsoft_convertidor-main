namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosDocumentosOpResponseDto
    {
        public int CodigoImpuestoDocumentoOp { get; set; }
        public int CodigoDocumentoOp { get; set; }
        public int CodigoRetencion { get; set; }
        public string DescripcionCodigoRetencion { get; set; } = string.Empty;
        public int TipoRetencionId { get; set; }
        public string DescripcionTipoRetencion { get; set; } = string.Empty;
        public string PeriodoImpositivo { get; set; } = string.Empty;
        public decimal BaseImponible { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoImpuestoExento { get; set; }
        public decimal MontoRetenido { get; set; }
   
    }
}
