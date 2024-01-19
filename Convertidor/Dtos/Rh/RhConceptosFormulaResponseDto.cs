namespace Convertidor.Dtos.Rh
{
	public class RhConceptosFormulaResponseDto
	{
		
		
		public int CodigoFormulaConcepto { get; set; }
		public int CodigoConcepto { get; set; }
		public decimal Porcentaje { get; set; }
		public decimal MontoTope { get; set; }
		public string TipoSueldo { get; set; } = string.Empty;
		public DateTime FechaDesde { get; set; }
		public string FechaDesdeString { get; set; }
		public FechaDto FechaDesdeObj { get; set; }
		public DateTime FechaHasta { get; set; }
		public string FechaHastaString { get; set; }
		public FechaDto FechaHastaObj { get; set; }
		public decimal PorcentajePatronal { get; set; }
    
        
        }

    }

   

 


