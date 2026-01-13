using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Dtos.Catastro
{
    public class CatDescriptivasResponseDto
    {
        public int Id { get; set; }
        public int DescripcionId { get; set; }
        public int DescripcionFkId { get; set; }
        public int TituloId { get; set; }
        public string DescripcionTitulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Codigo { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string Extra4 { get; set; } = string.Empty;
        public string Extra5 { get; set; } = string.Empty;
        public string Extra6 { get; set; } = string.Empty;
        public string Extra7 { get; set; } = string.Empty;
        public string Extra8 { get; set; } = string.Empty;
        public string Extra9 { get; set; } = string.Empty;
        public string Extra10 { get; set; } = string.Empty;
        public string Extra11 { get; set; } = string.Empty;
        public string Extra12 { get; set; } = string.Empty;
        public string Extra13 { get; set; } = string.Empty;
        public string Extra14 { get; set; } = string.Empty;
        public string Extra15 { get; set; } = string.Empty;
        public List<CatDescriptivasResponseDto>? ListaDescriptiva { get; set; }
    }
}
