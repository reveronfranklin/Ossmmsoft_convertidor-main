namespace Convertidor.Dtos.Cnt
{
    public class CntComprobantesUpdateDto
    {
        public int CodigoComprobante { get; set; }
        public int CodigoPeriodo { get; set; }
        public int TipoComprobanteId { get; set; }
        public string NumeroComprobante { get; set; } = string.Empty;
        public DateTime FechaComprobante { get; set; }
        public int OrigenId { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
