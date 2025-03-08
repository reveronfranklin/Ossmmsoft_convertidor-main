namespace Convertidor.Dtos.Rh
{
    public class RhDocumentosAdjuntosUpdateDto
    {

        public int CodigoDocumentoAdjunto { get; set; }
        public int CodigoDocumento { get; set; }
        public string Adjunto { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        
    }
    public class RhDocumentosFilesUpdateDto
    {
        
     
        public int CodigoDocumentoAdjunto { get; set; }
        public int CodigoDocumento { get; set; }
        public string Adjunto { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public List<IFormFile> Files { get; set; }
        
    }
   
    
    
 
	
}

