namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_PERIODOS
    {
        public  int CODIGO_PERIODO { get; set; }
        public string NOMBRE_PERIODO { get; set; } = string.Empty;
        public DateTime FECHA_DESDE { get; set; }
        public DateTime FECHA_HASTA { get; set; }
        public int ANO_PERIODO { get; set; }
        public int NUMERO_PERIODO { get; set; }
        public int USUARIO_PRECIERRE { get; set; }
        public DateTime FECHA_PRECIERRE { get; set; }
        public int USUARIO_CIERRE { get; set; }
        public DateTime FECHA_CIERRE { get; set; }
        public int USUARIO_PRECIERRE_CONC { get; set; }
        public DateTime FECHA_PRECIERRE_CONC { get; set; }
        public int USUARIO_CIERRE_CONC { get; set; }
        public DateTime FECHA_CIERRE_CONC { get; set; }
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
