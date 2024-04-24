namespace Convertidor.Dtos.Bm
{
	public class BmConteoResponseDto
	{

		public int CodigoBmConteo { get; set; } 
		public string Titulo { get; set; } = string.Empty;
		public int CodigoPersonaResponsable { get; set; } 
		public string NombrePersonaResponsable { get; set; } = string.Empty;
		public int ConteoId { get; set; } 
		public DateTime Fecha { get; set; }
		public string FechaString { get; set; }
		public FechaDto FechaObj { get; set; }
		public List<BmConteoDetalleResumenResponseDto> ResumenConteo { get; set; }
		public string FechaView { get { return $"{FechaObj.Day}-{FechaObj.Month}-{FechaObj.Year}"; } }
		
		public decimal TotalCantidad { get; set; }
		public decimal TotalCantidadContado { get; set; }
		public decimal TotalDiferencia { get { return TotalCantidad-TotalCantidadContado; } }
		public string Comentario { get; set; } = string.Empty;

    }
}

