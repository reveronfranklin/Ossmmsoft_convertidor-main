using System;
namespace Convertidor.Dtos.Bm
{
	public class BmConteoDetalleResumenResponseDto
	{
		
		public int CodigoBmConteo { get; set; }
		public int Conteo { get; set; }
		public string Descripcion { get { return $"CONTEO: {Conteo}"; } }
		public decimal Cantidad { get; set; }
		public decimal CantidadContada { get; set; }
		public decimal Diferencia  { get { return Cantidad-CantidadContada; } }
	}
}

