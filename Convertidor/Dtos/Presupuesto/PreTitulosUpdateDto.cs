namespace Convertidor.Dtos.Presupuesto
{
	public class PreTitulosUpdateDto
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

