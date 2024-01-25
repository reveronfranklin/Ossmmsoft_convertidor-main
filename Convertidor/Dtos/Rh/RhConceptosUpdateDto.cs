namespace Convertidor.Dtos.Rh;

public class RhConceptosUpdateDto
{
    public int CodigoConcepto { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int CodigoTipoNomina { get; set; }
    public string Denominacion { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string TipoConcepto { get; set; } = string.Empty;
    public int ModuloId { get; set; }
    public int CodigoPuc { get; set; }
    public string Status { get; set; }
    public string Extra1 { get; set; } = string.Empty;
    public int FrecuenciaId { get; set; }
    public int Dedusible { get; set; }
    public int Automatico { get; set; }
    public int IdModeloCalculo { get; set; }
    

}