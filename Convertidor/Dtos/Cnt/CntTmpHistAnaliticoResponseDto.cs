namespace Convertidor.Dtos.Cnt
{
    public class CntTmpHistAnaliticoResponseDto
    {
        public int CodigoHistAnalitico { get; set; }
        public int CodigoSaldo { get; set; }
        public int CodigoDetalleComprobante { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
