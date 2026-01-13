namespace Convertidor.Dtos.Cnt
{
    public class CntAuxiliaresPucUpdateDto
    {
        public int CodigoAuxiliarPuc { get; set; }
        public int CodigoAuxiliar { get; set; }
        public int CodigoPuc { get; set; }
        public string TipoDocumentoId { get; set; } = string.Empty;
    }
}
