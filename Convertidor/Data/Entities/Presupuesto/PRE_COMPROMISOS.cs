namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_COMPROMISOS
    {
        public int CODIGO_COMPROMISO { get; set; }
        public int ANO { get; set; }
        public int CODIGO_SOLICITUD { get; set; }
        public string NUMERO_COMPROMISO { get; set; } = string.Empty;
        public DateTime FECHA_COMPROMISO { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public DateTime? FECHA_ENTREGA { get; set; }
        public int CODIGO_DIR_ENTREGA { get; set; }
        public int? TIPO_PAGO_ID { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int? TIPO_RENGLON_ID { get; set; }
        public string NUMERO_ORDEN { get; set; } = string.Empty;
        public string SEARCH_TEXT { get; set; }
        public string MONTO_LETRAS { get; set; }
        public string FIRMANTE { get; set; }
        
        
    }
}
