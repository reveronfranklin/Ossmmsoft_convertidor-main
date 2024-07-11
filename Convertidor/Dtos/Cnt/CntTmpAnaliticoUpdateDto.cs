namespace Convertidor.Dtos.Cnt
{
    public class CntTmpAnaliticoUpdateDto
    {
        public int CodigoTmpAnalitico { get; set; }
        public int CodigoTmpSaldo { get; set; }
        public int CodigoDetalleComprobante { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}
