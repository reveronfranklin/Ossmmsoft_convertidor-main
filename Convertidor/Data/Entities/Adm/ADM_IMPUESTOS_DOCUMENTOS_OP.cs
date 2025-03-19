namespace Convertidor.Data.Entities.Adm
{
    public class ADM_IMPUESTOS_DOCUMENTOS_OP
    {
        public int CODIGO_IMPUESTO_DOCUMENTO_OP { get; set; }
        public int CODIGO_DOCUMENTO_OP { get; set; }
        public int CODIGO_RETENCION { get; set; }
        public int TIPO_RETENCION_ID { get; set; }
        public int TIPO_IMPUESTO_ID { get; set; }
        public string PERIODO_IMPOSITIVO { get; set; } = string.Empty;
        public decimal BASE_IMPONIBLE { get; set; }
        public decimal MONTO_IMPUESTO { get; set; }
        public decimal MONTO_IMPUESTO_EXENTO { get; set; }
        public decimal MONTO_RETENIDO { get; set; }
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
