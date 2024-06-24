namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_BANCO_ARCHIVO
    {
        public int CODIGO_BANCO_ARCHIVO { get; set; }
        public int CODIGO_BANCO_ARCHIVO_CONTROL { get; set; }
        public string NUMERO_BANCO { get; set; } = string.Empty;
        public string NUMERO_CUENTA { get; set; } = string.Empty;
        public DateTime FECHA_TRANSACCION { get; set; }
        public string NUMERO_TRANSACCION { get; set; } = string.Empty;
        public int TIPO_TRANSACCION_ID { get; set; }
        public string TIPO_TRANSACCION { get; set; } = string.Empty;
        public string DESCRIPCION_TRANSACCION { get; set; } = string.Empty;
        public int MONTO_TRANSACCION { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_DETALLE_EDO_CTA { get; set; }
    }
}
