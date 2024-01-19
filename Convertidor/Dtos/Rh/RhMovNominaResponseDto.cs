namespace Convertidor.Dtos.Rh
{
	public class RhMovNominaResponseDto
	{
		
		public int CodigoMovNomina { get; set; }
		public int CodigoTipoNomina { get; set; }
		public int CodigoPersona { get; set; }
		public int CodigoConcepto { get; set; }
		public string DescripcionConcepto { get; set; }
		public string ComplementoConcepto { get; set; } = string.Empty;
		public string Tipo { get; set; } = string.Empty;
		public string DescripcionTipo { get; set; } = string.Empty;
		public int FrecuenciaId { get; set; }
		public string DescripcionFrecuencia { get; set; } = string.Empty;
		public decimal Monto { get; set; }
		public decimal OssMonto { get; set; }
		
		
    
        
        }

    }

   

 


