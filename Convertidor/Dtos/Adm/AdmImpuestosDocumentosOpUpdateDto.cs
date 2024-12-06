namespace Convertidor.Dtos.Adm
{
    public class AdmImpuestosDocumentosOpUpdateDto
    {
        public int CodigoImpuestoDocumentoOp { get; set; }
        public int CodigoDocumentoOp { get; set; }
        public int CodigoRetencion { get; set; }
        public int TipoRetencionId { get; set; }
        public int BaseImponible { get; set; }
        public int MontoImpuesto { get; set; }
        public int MontoImpuestoExento { get; set; }
        public int MontoRetenido { get; set; }
   
    }
}
