namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_DETALLE_COMPROBANTE
    {
        public int CODIGO_DETALLE_COMPROBANTE { get; set; }
        public int CODIGO_COMPROBANTE { get; set; }
        public int CODIGO_MAYOR { get; set; }
        public int CODIGO_AUXILIAR { get; set; }
        public string REFERENCIA1 { get; set; } = string.Empty;
        public string REFERENCIA2 { get; set; } = string.Empty;
        public string REFERENCIA3 { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
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
