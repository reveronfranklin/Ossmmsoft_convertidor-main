using System;
namespace Convertidor.Dtos.Bm
{
	public class BmConteoDetalleResponseDto
	{
		
		
		public int CodigoBmConteoDetalle { get; set; }
		public int CodigoBmConteo { get; set; }
		public int Conteo { get; set; }
		public int CodigoIcp { get; set; }
		public string UnidadTrabajo { get; set; } = string.Empty;
		public string CodigoGrupo { get; set; } = string.Empty;
		public string CodigoNivel1 { get; set; } = string.Empty;
		public string CodigoNivel2 { get; set; } = string.Empty;
		public string NumeroLote { get; set; } = string.Empty;
		public int Cantidad { get; set; }
		public int CantidadContada { get; set; }
		public int Diferencia { get; set; }
		public string NumeroPlaca { get; set; } = string.Empty;
		public decimal ValorActual { get; set; } 
		public string Articulo { get; set; } = string.Empty;
		public string Especificacion { get; set; } = string.Empty;
		public string Servicio { get; set; } = string.Empty;
		public string ResponsableBien { get; set; } = string.Empty;
		public DateTime FechaMovimiento { get; set; }
		public string FechaMovimientoString { get; set; }
		public FechaDto FechaMovimientoObj { get; set; }
		public int CodigoBien { get; set; }
		public int CodigoMovBien { get; set; }
		public DateTime Fecha { get; set; }
		public string FechaString { get; set; }
		public FechaDto FechaObj { get; set; }
		public string Comentario { get; set; } = string.Empty;
		
		public string CodigoPlaca { get { return $"{CodigoGrupo}-{CodigoNivel1}-{CodigoNivel2}-{NumeroPlaca}"; } }
	
	}
}

