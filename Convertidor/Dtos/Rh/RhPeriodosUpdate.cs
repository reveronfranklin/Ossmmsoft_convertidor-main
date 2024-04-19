namespace Convertidor.Dtos.Rh;

public class RhPeriodosUpdate
{
    public int CodigoPeriodo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int CodigoTipoNomina { get; set; }
    public DateTime FechaNomina { get; set; }
    public int Periodo { get; set; }
    public string TipoNomina { get; set; }
    

    
    
}