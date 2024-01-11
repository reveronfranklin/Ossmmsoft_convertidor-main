namespace Convertidor.Dtos.Rh
{
	public class RhConceptosAcumulaUpdateDto
	{
		
		public int CodigoConceptoAcumula { get; set; }
		public int CodigoConcepto { get; set; }
		public string TipoAcumuladoId { get; set; }=String.Empty;
		public int CodigoConceptoAsociado { get; set; }
		public DateTime FechaDesde { get; set; }
		public string FechaDesdeString { get; set; }
		public FechaDto FechaDesdeObj { get; set; }
		public DateTime FechaHasta { get; set; }
		public string FechaHastaString { get; set; }
		public FechaDto FechaHastaObj { get; set; }
		
    
        
        }

    }

   

 


