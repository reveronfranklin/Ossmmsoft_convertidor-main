namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_ARRENDAMIENTOS_INMUEBLES
    {
        public int CODIGO_ARRENDAMIENTO_INMUEBLE { get; set; }
        public int CODIGO_INMUEBLE { get; set; }
        public string NUMERO_DE_EXPEDIENTE { get; set; } = string.Empty;
        public DateTime FECHA_DONACION { get; set; }
        public string NUMERO_CONSECION_DE_USO { get; set; } = string.Empty;
        public string NUMERO_RESOLUCION_X_RESICION { get; set; } = string.Empty;
        public DateTime FECHA_RESOLUCION_X_RESICION { get; set; }
        public string NUMERO_DE_INFORME { get; set; }
        public DateTime FECHA_INICIO_CONTRATO { get; set; }
        public DateTime FECHA_FIN_CONTRATO { get; set; }
        public string NUMERO_RESOLUCION { get; set; } = string.Empty;
        public DateTime FECHA_RESOLUCION { get; set; }
        public string NUMERO_NOTIFICACION { get; set; } = string.Empty;
        public decimal CANON { get; set; } 
        public string TOMO { get; set; } = string.Empty;
        public string FOLIO { get; set; } = string.Empty;
        public string REGISTRO { get; set; } = string.Empty;
        public int CODIGO_CONTRIBUYENTE { get; set; }
        public string NUMERO_CONTRATO { get; set; } = string.Empty;
        public string OBSERVACIONES { get; set; } = string.Empty;
        public string NUMERO_ARRENDAMIENTO { get; set; } = string.Empty;
        public string TIPO_TRANSACCION { get; set; } = string.Empty;
        public decimal TRIBUTO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
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
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal VALOR_TERRENO { get; set; }
        public string NUMERO_ACUERDO { get; set; } = string.Empty;
        public DateTime FECHA_ACUERDO { get; set; }
        public string CODIGO_CATASTRO { get; set; }
        public DateTime FECHA_NOTIFICACION { get; set; }

    }
}
