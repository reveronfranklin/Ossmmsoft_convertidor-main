namespace Convertidor.Dtos.Adm;

public class AdmRetencionesUpdateDto
{
    public int CodigoRetencion { get; set; }
    public int TipoRetencionId { get; set; }
    public string? ConceptoPago { get; set; }=string.Empty;
    public string? Codigo { get; set; }=string.Empty;
    public decimal? BaseImponible { get; set; }
    public decimal? PorRetencion { get; set; }
    public decimal? MontoRetencion { get; set; }
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }
   
}