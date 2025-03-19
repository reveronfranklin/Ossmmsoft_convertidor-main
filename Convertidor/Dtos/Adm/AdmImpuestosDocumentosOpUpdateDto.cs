namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosDocumentosOpUpdateDto
    {
        public int CodigoImpuestoDocumentoOp { get; set; }
        public int CodigoDocumentoOp { get; set; }
        public int CodigoRetencion { get; set; }
        public int TipoRetencionId { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoImpuestoExento { get; set; }
        public decimal MontoRetenido { get; set; }
   
    }
}
