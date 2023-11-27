using System;
using System.Net.NetworkInformation;

namespace Convertidor.Dtos.Rh
{
	public class RhComunicacionResponseDto
	{
        
        public int CodigoComunicacion { get; set; }
        public int CodigoPersona { get; set; }
        public int TipoComunicacionId { get; set; }  
        public string DescripcionTipoComunicacion { get; set; } = string.Empty;
        public string CodigoArea { get; set; } = string.Empty;
        public string LineaComunicacion { get; set; } = string.Empty;
        public int Extencion { get; set; }
        public bool Principal { get; set; } 
     
    }

   

 
}

