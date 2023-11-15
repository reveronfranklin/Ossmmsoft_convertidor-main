namespace Convertidor.Dtos.Rh;

public class RhResumenPagoPorPersona
{
    public int  CodigoTipoNomina{ get; set; }
    public string  TipoNomina{ get; set; }
    public int Cedula { get; set; }
    public DateTime FechaNomina { get; set; } 
    public string FechaNominaString { get; set; } 
    public string  LinkData{ get; set; }
    
}