namespace Convertidor.Dtos.Adm
{
    public class AdmComprobantesDocumentosOpResponseDto
    {
        public int CodigoComprobanteDocOp { get; set; }
        public int CodigoDocumentoOp { get; set; }
        public int CodigoOrdenPago { get; set; }
        public int CodigoProveedor { get; set; }
        public float NumeroComprobante { get; set; }
        public DateTime FechaComprobante { get; set; }
        public string FechaComprobanteString { get; set; } 
        public FechaDto FechaComprobanteObj { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }

    }
}
