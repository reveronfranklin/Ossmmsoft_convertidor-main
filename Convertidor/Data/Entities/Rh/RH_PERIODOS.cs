namespace Convertidor.Data.Entities.Rh
{
	public class RH_PERIODOS
	{

        public int CODIGO_PERIODO { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public int PERIODO { get; set; }
        public string TIPO_NOMINA { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int USUARIO_PRECIERRE { get; set; }
        public DateTime? FECHA_PRECIERRE { get; set; }
        public int USUARIO_CIERRE { get; set; }
        public DateTime? FECHA_CIERRE { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_CUENTA_EMPRESA { get; set; }
        public int USUARIO_PRENOMINA { get; set; }
        public DateTime? FECHA_PRENOMINA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;


    }
}

