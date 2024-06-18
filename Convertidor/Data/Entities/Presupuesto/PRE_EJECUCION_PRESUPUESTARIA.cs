namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_EJECUCION_PRESUPUESTARIA
    {
        public int CODIGO_EJE_PRESUPUESTARIA { get; set; }
        public string CODIGO_GRUPO { get; set; }
        public string CODIGO_NIVEL1 { get; set; } = string.Empty;
        public string CODIGO_NIVEL2 { get; set; } = string.Empty;
        public string CODIGO_NIVEL3 { get; set; } = string.Empty;
        public string CODIGO_NIVEL4 { get; set; } = string.Empty;
        public decimal I_REAL { get; set; }
        public decimal I_PROYECTADO { get; set; }
        public decimal II_REAL { get; set; }
        public decimal II_PROYECTADO { get; set; }
        public decimal III_REAL { get; set; }
        public decimal III_PROYECTADO { get; set; }
        public decimal IV_REAL { get; set; }
        public decimal IV_PROYECTADO { get; set; }
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
