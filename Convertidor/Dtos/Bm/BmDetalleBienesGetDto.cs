using System;
namespace Convertidor.Dtos.Bm
{
	public class BmDetalleBienesGetDto
	{

        public int CodigoDetalleBien { get; set; }
        public int CodigoBien { get; set; }
        public int TipoEspecificacionId { get; set; }
        public int EspecificacionId { get; set; }
        public string Especificacion { get; set; }=string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        
    }
}

