namespace Convertidor.Dtos.Cnt
{
    public class CntRelacionDocumentosResponseDto
    {
        public int CodigoRelacionDocumento { get; set; }
        public int TipoDocumentoId { get; set; }
        public int TipoTransaccionId { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
