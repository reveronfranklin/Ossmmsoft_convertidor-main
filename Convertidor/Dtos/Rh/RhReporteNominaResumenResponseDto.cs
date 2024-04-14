namespace Convertidor.Dtos.Rh;

public class RhReporteNominaResumenResponseDto
{
    public int Id { get; set; }
    public int CodigoTipoNomina { get; set; }
    public int CodigoPeriodo { get; set; }
    public int Periodo { get; set; }
    public DateTime FechaNomina { get; set; }
    public string FechaNominaString { get; set; }
    public FechaDto FechaNominaObj { get; set; }
    public string CodigoIcpConcat { get; set; }
    public int CodigoIcp { get; set; }
    public string Denominacion { get; set; }
    public int CodigoPersona { get; set; }
    public string Nombre { get; set; }
    public string DescripcionPeriodo { get; set; }
    
  
    
}
public class RhListOficinaDto
{
  
    public string CodigoIcpConcat { get; set; }
    public int CodigoIcp { get; set; }
    public string Denominacion { get; set; }
   
    
  
    
}