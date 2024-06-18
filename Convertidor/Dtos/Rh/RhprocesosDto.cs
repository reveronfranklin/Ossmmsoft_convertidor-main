namespace Convertidor.Dtos.Rh
{
	public class RhProcesosDto
	{
        public int CodigoProceso { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public List<ListConceptosDto> Conceptos { get; set; }
    }
}

