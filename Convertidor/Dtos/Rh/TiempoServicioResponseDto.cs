namespace Convertidor.Dtos.Rh
{
	public class TiempoServicioResponseDto
	{
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public int CantidadAños { get; set; }
        public int CantidadMeses { get; set; }
        public int CantidadDias { get; set; }

        public string FechaDesdeString { get; set; } = string.Empty;
        public string FechaHastaString { get; set; } = string.Empty;
    }
}

