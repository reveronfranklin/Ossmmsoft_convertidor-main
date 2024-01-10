using System;
namespace Convertidor.Dtos.Bm
{
	public class BmDescriptivasGetDto
	{

        public int DescripcionId { get; set; }
        public int DescripcionIdFk { get; set; }
        public int TituloId { get; set; }
        public string DescripcionTitulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;

        public List<BmDescriptivasGetDto>? ListaDescriptiva { get; set; }

    }
}

