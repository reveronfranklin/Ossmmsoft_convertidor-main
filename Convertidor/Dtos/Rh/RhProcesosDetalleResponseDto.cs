namespace Convertidor.Dtos.Rh
{
	public class RhProcesosDetalleResponseDtoDto
	{
		public int CodigoDetalleProceso { get; set; }
		public int CodigoProceso { get; set; } 
		public int CodigoConcepto { get; set; }
		public string DescripcionConcepto { get; set; }
		public int CodigoTipoNomina { get; set; }
		public string DescripcionTipoNomina { get; set; }
    }
}

