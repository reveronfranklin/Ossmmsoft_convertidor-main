namespace Convertidor.Dtos.Adm;

public class AdmRetencionesResponseDto
{
    public int CodigoRetencion { get; set; }
    public int TipoRetencionId { get; set; }
    public string? DescripcionTipoRetencion { get; set; }=string.Empty;
    public string? ConceptoPago { get; set; }=string.Empty;
    public int? TipoPersonaId { get; set; }
    public string? DescripcionTipoPersona { get; set; }=string.Empty;
    public decimal? BaseImponible { get; set; }
    public decimal? PorRetencion { get; set; }
    public decimal? MontoRetencion { get; set; }
    public DateTime? FechaIni { get; set; }
    public string FechaIniString { get; set; }
    public FechaDto FechaIniObj { get; set; }
    public DateTime? FechaFin { get; set; }
    public string FechaFinString { get; set; }
    public FechaDto FechaFinObj { get; set; }
   
}