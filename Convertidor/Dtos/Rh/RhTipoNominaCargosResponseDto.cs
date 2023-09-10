namespace Convertidor.Dtos.Rh;

public class 
    RhTipoNominaCargosResponseDto
{
    public int   CodigoTipoNomina { get; set; }
    public string TipoNomina { get; set; } = string.Empty;
    public int   CodigoCargo { get; set; }
    public string  DescripcionCargo { get; set; }= string.Empty;
    
    public string  Desde { get; set; }= string.Empty;
    
    public string  Hasta { get; set; }= string.Empty;
    public string  Color { get; set; }= string.Empty;
  
}