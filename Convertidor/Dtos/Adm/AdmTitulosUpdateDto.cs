using System;
namespace Convertidor.Dtos.Adm
{
	public class AdmTitulosUpdateDto
	{
        public int TituloId { get; set; }
        public int TituloIdFk { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
    }
}

