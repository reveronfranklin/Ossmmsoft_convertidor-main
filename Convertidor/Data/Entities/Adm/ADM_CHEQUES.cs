namespace Convertidor.Data.Entities.Adm
{
    public class ADM_CHEQUES
    {
        public int CODIGO_CHEQUE { get; set; }
        public Int16 ANO { get; set; }
        public int CODIGO_CUENTA_BANCO { get; set; }
        public int NUMERO_CHEQUERA { get; set; }
        public int NUMERO_CHEQUE { get; set; }
        public DateTime FECHA_CHEQUE { get; set; }
        public DateTime FECHA_CONCILIACION { get; set; }
        public DateTime FECHA_ANULACION { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public int CODIGO_CONTACTO_PROVEEDOR { get; set; }
        public Int16 PRINT_COUNT { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public string ENDOSO { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public string TIPO_BENEFICIARIO { get; set; } = string.Empty;
        public int TIPO_CHEQUE_ID { get; set; }
        public DateTime FECHA_ENTREGA { get; set; }
        public int USUARIO_ENTREGA { get; set; }
    }
}
