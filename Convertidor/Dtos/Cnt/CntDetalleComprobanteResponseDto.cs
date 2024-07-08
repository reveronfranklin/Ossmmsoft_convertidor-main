namespace Convertidor.Dtos.Cnt
{
    public class CntDetalleComprobanteResponseDto
    {
        public int CodigoDetalleComprobante { get; set; }
        public int CodigoComprobante { get; set; }
        public int CodigoMayor { get; set; }
        public int CodigoAuxiliar { get; set; }
        public string Referencia1 { get; set; } = string.Empty;
        public string Referencia2 { get; set; } = string.Empty;
        public string Referencia3 { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Monto { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
