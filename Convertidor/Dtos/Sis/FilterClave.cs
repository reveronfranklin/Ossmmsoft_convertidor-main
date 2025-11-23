namespace Convertidor.Dtos.Sis
{
	public class FilterClave
	{
		public string Clave { get; set; } = string.Empty;
	}
}


public class FiltersEstado
	{
		public int CodigoPais { get; set; }
	}

	public class FiltersMunicipio
	{
		public int CodigoPais { get; set; }
		public int CodigoEstado { get; set; }
	}

		public class FiltersCiudad
	{
		public int CodigoPais { get; set; }
		public int CodigoEstado { get; set; }
		public int CodigoMunicipio { get; set; }
	}

	public class FiltersParroquia
	{
		public int CodigoPais { get; set; }
		public int CodigoEstado { get; set; }
		public int CodigoMunicipio { get; set; }
		public int CodigoCiudad { get; set; }
	}