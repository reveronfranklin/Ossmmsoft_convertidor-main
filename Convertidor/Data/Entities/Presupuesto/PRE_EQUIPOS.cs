namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_EQUIPOS
    {
        public int CODIGO_EQUIPO { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int PRINCIPAL { get; set; }
        public string DENOMINACION { get; set; }= string.Empty;
        public int REEMPLAZOS { get; set; }
        public int DEFICIENCIAS { get; set; }
        public decimal COSTO_UNITARIO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
    }
}
