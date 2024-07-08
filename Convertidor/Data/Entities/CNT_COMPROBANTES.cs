namespace Convertidor.Data.Entities
{
    public class CNT_COMPROBANTES
    {
        public int CODIGO_COMPROBANTE { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public int TIPO_COMPROBANTE_ID { get; set; }
        public string NUMERO_COMPROBANTE { get; set; }=string.Empty;
        public DateTime FECHA_COMPROBANTE { get; set; }
        public int ORIGEN_ID { get; set; }
        public string OBSERVACION { get; set; } = string.Empty;
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
