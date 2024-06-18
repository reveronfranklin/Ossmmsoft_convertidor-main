namespace Convertidor.Data.Entities.Rh
{
	public class RH_FAMILIARES
	{
		public int CODIGO_PERSONA { get; set; }
		public int	CODIGO_FAMILIAR { get; set; }
		public int	 CEDULA_FAMILIAR { get; set; }
		public string NACIONALIDAD { get; set; } = string.Empty;
		public string NOMBRE { get; set; } = string.Empty;
		public string APELLIDO { get; set; } = string.Empty;
		public DateTime FECHA_NACIMIENTO { get; set; }
		public int	PARIENTE_ID { get; set; }
		public string SEXO { get; set; } = string.Empty;
		public int	NIVEL_EDUCATIVO_ID { get; set; }
		public int	GRADO { get; set; }
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

