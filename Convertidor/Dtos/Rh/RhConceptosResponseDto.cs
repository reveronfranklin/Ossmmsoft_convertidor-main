namespace Convertidor.Dtos.Rh
{
    public class RhConceptosResponseDto
    {
        public int CodigoConcepto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int CodigoTipoNomina { get; set; }
        public string TipoNominaDescripcion { get; set; } = string.Empty;

        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string TipoConcepto { get; set; } = string.Empty;
        public int ModuloId { get; set; }
        public string ModuloDescripcion { get; set; }
        public int CodigoPuc { get; set; }
        public string CodigoPucConcat { get; set; }
        public string Status { get; set; }
        public int FrecuenciaId { get; set; }
        public string FrecuenciaDescripcion { get; set; }
        public int Dedusible { get; set; }
        public int Automatico { get; set; }
        public int IdModeloCalculo { get; set; }
        public string Extra1 { get; set; } = string.Empty;


    }

}






