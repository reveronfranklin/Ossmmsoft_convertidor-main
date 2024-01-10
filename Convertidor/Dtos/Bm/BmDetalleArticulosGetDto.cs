using System;
namespace Convertidor.Dtos.Bm
{
	public class BmDetalleArticulosGetDto
	{

        public int CodigoDetalleArticulo { get; set; }
        public int CodigoArticulo { get; set; }
        public int TipoEspecificacionId { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        
    }
}

