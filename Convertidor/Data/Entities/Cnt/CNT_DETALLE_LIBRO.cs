namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_DETALLE_LIBRO
    {
        public int CODIGO_DETALLE_LIBRO { get; set; }
        public int CODIGO_LIBRO { get; set; }
        public int TIPO_DOCUMENTO_ID { get; set; }
        public int CODIGO_CHEQUE { get; set; }
        public int CODIGO_IDENTIFICADOR { get; set; }
        public int ORIGEN_ID { get; set; }
        public string NUMERO_DOCUMENTO { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int MONTO { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2{ get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}
