namespace Convertidor.Dtos.Bm
{
    public class BmBienesFotoUpdateDto
    {

        public int CodigoBienFoto { get; set; }
        public int CodigoBien { get; set; }
        public string NumeroPlaca { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        
    }
    public class BmBienesimageUpdateDto
    {
        
     
        public int CodigoBien { get; set; }
        public string NumeroPlaca { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public List<IFormFile> Files { get; set; }
        
    }
   
    
    
 
	
}

