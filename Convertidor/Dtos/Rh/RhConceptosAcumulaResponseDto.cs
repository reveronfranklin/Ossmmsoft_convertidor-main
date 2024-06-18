namespace Convertidor.Dtos.Rh
{
	public class RhConceptosAcumulaResponseDto
	{
		
		public int CodigoConceptoAcumula { get; set; }
		public int CodigoConcepto { get; set; }
		public string TipoAcumuladoId { get; set; }=String.Empty;
		public string TipoAcumuladoDescripcion { get; set; }=String.Empty;
		public int CodigoConceptoAsociado { get; set; }
		public string CodigoConceptoAsociadoDescripcion { get; set; }=String.Empty;
		public DateTime FechaDesde { get; set; }
		public string FechaDesdeString { get; set; }
		public FechaDto FechaDesdeObj { get; set; }
		public DateTime FechaHasta { get; set; }
		public string FechaHastaString { get; set; }
		public FechaDto FechaHastaObj { get; set; }
		
    
        
        }

    }

   

 


