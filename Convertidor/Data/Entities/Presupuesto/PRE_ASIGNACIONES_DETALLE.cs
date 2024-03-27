namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_ASIGNACIONES_DETALLE
	{
		
		public int CODIGO_ASIGNACION { get; set; }
        public int CODIGO_ASIGNACION_DETALLE { get; set; }
        public DateTime FECHA_DESEMBOLSO { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public decimal MONTO { get; set; }
        public string NOTAS { get; set; } = string.Empty;
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

