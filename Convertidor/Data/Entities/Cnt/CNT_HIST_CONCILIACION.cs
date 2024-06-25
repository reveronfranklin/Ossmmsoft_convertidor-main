namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_HIST_CONCILIACION
    {
        public int CODIGO_HIST_CONCILIACION { get; set; }
        public int CODIGO_CONCILIACION { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public int CODIGO_CUENTA_BANCO { get; set; }
        public int CODIGO_DETALLE_LIBRO { get; set; }
        public int CODIGO_DETALLE_EDO_CTA { get; set; }
        public DateTime FECHA { get; set; }
        public string NUMERO { get; set; } = string.Empty;
        public decimal MONTO { get; set; }
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
