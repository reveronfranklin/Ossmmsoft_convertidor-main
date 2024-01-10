namespace Convertidor.Dtos.Rh
{
	public class ListPersonasMovControl
    {
        public int CodigoPersonaMovCtrl { get; set; }
        public int CodigoPersona { get; set; }
        public int CodigoConcepto { get; set; }
        public int ControlAplica { get; set; }
        public List<ListConceptosDto> Conceptos { get; set; }
        public TiempoServicioResponseDto? TiempoServicio { get; set; }
    }
}

