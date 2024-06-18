namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_ASIGNACIONES
	{

        public int CODIGO_ASIGNACION { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_PUC { get; set; }
        public decimal ORDINARIO { get; set; }
        public decimal COORDINADO { get; set; }
        public decimal LAEE { get; set; }
        public decimal FIDES { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;

        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public decimal PRESUPUESTADO { get; set; }

        public decimal TOTAL_DESEMBOLSO { get; set; } = 0;




	}
}

