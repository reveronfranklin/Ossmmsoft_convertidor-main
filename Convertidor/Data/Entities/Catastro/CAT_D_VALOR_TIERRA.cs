namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_D_VALOR_TIERRA
    {
        public int CODIGO_VALOR_TIERRA { get; set; }
        public int CODIGO_D_VALOR_TIERRA_URB_FK { get; set; } 
        public int PAIS_ID { get; set; }
        public int ESTADO_ID { get; set; }
        public int MUNICIPIO_ID { get; set; }
        public int PARROQUIA_ID { get; set; }
        public int SECTOR_ID { get; set; }
        public DateTime FECHA_INI_VIG_VALOR { get; set; }
        public DateTime FECHA_FIN_VIG_VALOR { get; set; } 
        public int VIALIDAD_PRINCIPAL_ID { get; set; }
        public int VIALIDAD_DESDE_ID { get; set; }
        public int VIALIDAD_HASTA_ID { get; set; }
        public decimal VALOR_TIERRA { get; set; }
        public string OBSERVACIONES { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_ZONIFICACION_ID { get; set; }
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
    }
}
