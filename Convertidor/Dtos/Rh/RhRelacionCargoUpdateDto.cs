namespace Convertidor.Dtos.Rh
{
	public class RhRelacionCargoUpdateDto
	{
        public int CodigoRelacionCargo { get; set; }    
        public int CodigoCargo { get; set; }
        public int TipoNomina { get; set; }
        public int CodigoIcp { get; set; }

        public int CodigoPersona { get; set; }
 
        public decimal Sueldo { get; set; }
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public int CodigoRelacionCargoPre { get; set; }
    }
}

