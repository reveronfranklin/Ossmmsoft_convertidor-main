namespace Convertidor.Data.EntitiesDestino.ADM;


    public class ADM_COMPROBANTES_DOCUMENTOS_OP
    {
        public int CODIGO_COMPROBANTE_DOC_OP { get; set; }
        public int CODIGO_DOCUMENTO_OP { get; set; }
        public int CODIGO_ORDEN_PAGO { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public float NUMERO_COMPROBANTE { get; set; }
        public DateTime FECHA_COMPROBANTE { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }

    }
