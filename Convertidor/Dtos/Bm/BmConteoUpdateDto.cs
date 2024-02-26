using System;
namespace Convertidor.Dtos.Bm
{
	public class BmConteoUpdateDto
	{

		public int CodigoBmConteo { get; set; } 
		public string Titulo { get; set; } = string.Empty;
		public int CodigoPersonaResponsable { get; set; } 
		public int ConteoId { get; set; } 
		public DateTime Fecha { get; set; }
		public string FechaString { get; set; }
		public FechaDto FechaObj { get; set; }
		public string Comentario { get; set; } = string.Empty;

		public List<ICPGetDto> ListIcpSeleccionado { get; set; }

    }
}

