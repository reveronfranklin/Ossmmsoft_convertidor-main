namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_DOCUMENTOS_RAMO
    {
        public int CODIGO_DOCU_RAMO { get; set; }
        public int CODIGO_CONTRIBUYENTE { get; set; }
        public int CODIGO_CONTRIBUYENTE_FK { get; set; }
        public int TRIBUTO { get; set; } 
        public int CODIGO_IDENTIFICADOR { get; set; }
        public int CODIGO_ESTADO { get; set; }
        public int CODIGO_MUNICIPIO { get; set; }
        public int TIPO_DOCUMENTO_ID { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public DateTime FECHA_DOCUMENTO { get; set; }
        public string OBSERVACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string ORIGEN { get; set; } = string.Empty;
        public string TIPO_TRANSACCION { get; set; } = string.Empty;
        public int CODIGO_APLICACION { get; set; }
        public string EXTRA4 { get; set; } = string.Empty;
        public string EXTRA5 { get; set; } = string.Empty;
        public string EXTRA6 { get; set; } = string.Empty;
        public string EXTRA7 { get; set; } = string.Empty;
        public string EXTRA8 { get; set; } = string.Empty;
        public string EXTRA9 { get; set; } = string.Empty;
        public string EXTRA10 { get; set; } = string.Empty;
        public string EXTRA11 { get; set; } = string.Empty;
        public string EXTRA12 { get; set; } = string.Empty;
        public string EXTRA13 { get; set; } = string.Empty;
        public string EXTRA14 { get; set; } = string.Empty;
        public string EXTRA15 { get; set; } = string.Empty;
        public int CODIGO_FICHA { get; set; }
    }
}
