namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_AFOROS_INMUEBLES
    {
        public int CODIGO_AFORO_INMUEBLE { get; set; }
        public int TRIBUTO { get; set; }
        public int CODIGO_INMUEBLE { get; set; }
        public decimal MONTO { get; set; }
        public decimal MONTO_MINIMO { get; set; }
        public int CODIGO_FORMA_LIQUIDACION { get; set; }
        public int CODIGO_FORMA_LIQ_MINIMO { get; set; }
        public DateTime FECHA_LIQUIDACION { get; set; }
        public DateTime FECHA_PERIODO_INI { get; set; }
        public DateTime FECHA_PERIODO_FIN { get; set; }
        public int APLICADO_ID { get; set; }
        public int CODIGO_APLICADO { get; set; }
        public int ESTATUS { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public DateTime FECHA_INICIO_EXONERA { get; set; }
        public DateTime FECHA_FIN_EXONERA { get; set; }
        public string OBSERVACION { get; set; }
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
        public int CODIGO_AVALUO_CONSTRUCCION { get; set; }
        public int CODIGO_AVALUO_TERRENO { get; set; }

    }
}
