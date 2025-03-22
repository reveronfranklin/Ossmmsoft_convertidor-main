namespace Convertidor.Data.Entities.Adm
{
    public class ADM_CHEQUES
    {
        public int CODIGO_CHEQUE { get; set; } // NUMBER(10,0)
        public int ANO { get; set; } // NUMBER(4,0)
        public int CODIGO_CUENTA_BANCO { get; set; } // NUMBER(10,0)
        public int NUMERO_CHEQUERA { get; set; } // NUMBER(10,0)
        public int NUMERO_CHEQUE { get; set; } // NUMBER(10,0)
        public DateTime FECHA_CHEQUE { get; set; } // DATE
        public DateTime? FECHA_CONCILIACION { get; set; } // DATE (nullable)
        public DateTime? FECHA_ANULACION { get; set; } // DATE (nullable)
        public int CODIGO_PROVEEDOR { get; set; } // NUMBER(10,0)
        public int? CODIGO_CONTACTO_PROVEEDOR { get; set; } // NUMBER(10,0) (nullable)
        public int PRINT_COUNT { get; set; } // NUMBER(5,0)
        public string MOTIVO { get; set; } // VARCHAR2(2000)
        public string STATUS { get; set; } // CHAR(2) (nullable)
        public string? ENDOSO { get; set; } // CHAR(1) (nullable)
        public string EXTRA1 { get; set; } // VARCHAR2(100) (nullable)
        public string EXTRA2 { get; set; } // VARCHAR2(100) (nullable)
        public string EXTRA3 { get; set; } // VARCHAR2(100) (nullable)
        public int? USUARIO_INS { get; set; } // NUMBER(10,0) (nullable)
        public DateTime? FECHA_INS { get; set; } // DATE (nullable)
        public int? USUARIO_UPD { get; set; } // NUMBER(10,0) (nullable)
        public DateTime? FECHA_UPD { get; set; } // DATE (nullable)
        public int CODIGO_EMPRESA { get; set; } // NUMBER(10,0)
        public int CODIGO_PRESUPUESTO { get; set; } // NUMBER(10,0)
        public string? TIPO_BENEFICIARIO { get; set; } // CHAR(1) (nullable)
        public int TIPO_CHEQUE_ID { get; set; } // NUMBER(10,0)
        public DateTime? FECHA_ENTREGA { get; set; } // DATE (nullable)
        public int? USUARIO_ENTREGA { get; set; } // NUMBER(10,0) (nullable)
        public int CODIGO_LOTE_PAGO { get; set; } // NUMBER(10,0)
        public string? SEARCH_TEXT { get; set; } // CHAR(1) (nullable)
    }
}
