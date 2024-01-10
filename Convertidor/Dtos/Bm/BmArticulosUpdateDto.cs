using System;
namespace Convertidor.Dtos.Bm
{
    public class BmArticulosUpdateDto
    {

        public int CodigoArticulo { get; set; }
        public int CodigoClasificacionBien { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }

	
}

