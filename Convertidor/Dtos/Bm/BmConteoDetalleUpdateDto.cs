namespace Convertidor.Dtos.Bm
{
	public class BmConteoDetalleUpdateDto
	{
		public int CodigoBmConteoDetalle { get; set; }
		public int CantidadContada { get; set; }
		public string Comentario { get; set; }
		
		public bool ReplicarComentario { get; set; }
	
	}
}

