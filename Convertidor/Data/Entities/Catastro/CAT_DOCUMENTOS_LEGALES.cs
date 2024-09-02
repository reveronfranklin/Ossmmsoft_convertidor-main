namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_DOCUMENTOS_LEGALES
    {
        public int CODIGO_DOCUMENTOS_LEGALES { get; set; }
        public int CODIGO_FICHA { get; set; }
        public int DOCUMENTO_NUMERO { get; set; }
        public string FOLIO_NUMERO { get; set; } = string.Empty;
        public string TOMO_NUMERO { get; set; } = string.Empty;
        public string PROF_NUMERO { get; set; } = string.Empty;
        public DateTime FECHA_REGISTRO { get; set; }
        public int AREA_TERRENO { get; set; }
        public int AREA_CONSTRUCCION { get; set; }
        public string PROTOCOLO { get; set; } = string.Empty;
        public int MONTO_REGISTRO { get; set; }
        public int SERV_TERRENO { get; set; }
        public decimal PRECIO_TERRENO { get; set; }
        public int NUMERO_CIVICO { get; set; }
        public DateTime FECHA_PRIMERA_VISITA { get; set; }
        public DateTime FECHA_LEVANTAMIENTO { get; set; } 
        public int CONTROL_ARCHIVO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
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
