namespace Convertidor.Dtos.Rh
{
    public class RhDocumentosResponseDto
    {
        public int CodigoPersona { get; set; }
        public int CodigoDocumento { get; set; }
        public int TipodocumentoId { get; set; }
        public string DescripcionDocumento { get;set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string FechaVencimientoString { get; set; }
        public FechaDto FechaVencimientoObj { get; set; }
        public int TipoGradoid { get; set; }
        public int GradoId { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
      
       
        }
    }

   




   

 


