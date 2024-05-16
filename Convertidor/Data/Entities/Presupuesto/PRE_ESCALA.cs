namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_ESCALA
    {
        public int CODIGO_ESCALA { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public string NUMERO_ESCALA { get; set; } = string.Empty;
        public string CODIGO_GRUPO { get; set; }
        public decimal VALOR_INI { get; set; }
        public decimal VALOR_FIN { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;


    }
}
