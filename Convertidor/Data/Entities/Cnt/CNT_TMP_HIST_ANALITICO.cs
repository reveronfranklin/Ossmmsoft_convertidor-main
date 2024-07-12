namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_TMP_HIST_ANALITICO
    {
        public int CODIGO_HIST_ANALITICO { get; set; }
        public int CODIGO_SALDO { get; set; }
        public int CODIGO_DETALLE_COMPROBANTE { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}
