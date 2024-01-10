namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_V_DENOMINACION_PUC
    {
        
        public int ANO_SALDO { get; set; } 
        public int MES_SALDO { get; set; } 
        public int CODIGO_PRESUPUESTO { get; set; }
        public string CODIGO_PARTIDA { get; set; } = string.Empty;
        public string CODIGO_GENERICA { get; set; } = string.Empty;
        public string CODIGO_ESPECIFICA { get; set; } = string.Empty;
        public string CODIGO_SUBESPECIFICA { get; set; } = string.Empty;
        public string CODIGO_NIVEL5 { get; set; } = string.Empty;
        public string DENOMINACION_PUC { get; set; } = string.Empty;
        public decimal PRESUPUESTADO { get; set; }
        public decimal MODIFICADO { get; set; }
        public decimal COMPROMETIDO { get; set; }
        public decimal CAUSADO { get; set; }
        public decimal PAGADO { get; set; }
        public decimal DEUDA { get; set; }
        public decimal DISPONIBILIDAD { get; set; }
        public decimal DISPONIBILIDAD_FINAN { get; set; }

    }
}

