namespace Convertidor.Dtos.Rh;

public class RhReporteNominaResumenConceptoResponseDto
{

    public DateTime FechaNomina { get; set; }
    public string TipoNomina { get; set; }
    public string NumeroConcepto { get; set; }
    public string DenominacionConcepto { get; set; }
    public decimal Asignacion { get; set; }
    public decimal Deduccion { get; set; }
    public int Periodo { get; set; }
    
    public string Año { get; set; }
    public string Mes { get; set; }
    public string Descripcion { get; set; }
  
}
