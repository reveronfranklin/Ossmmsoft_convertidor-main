using System;
namespace Convertidor.Dtos.Rh
{
	public class RhprocesosDto
	{
        public int CodigoProceso { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public List<ListConceptosDto> Conceptos { get; set; }
    }
}

