namespace Convertidor.Dtos.Rh;

public class FilterRepoteNomina
{
    public int CodigoTipoNomina { get; set; }
    public int CodigoPeriodo { get; set; }
    public int? CodigoIcp { get; set; }
    public int? CodigoPersona { get; set; }
    public bool ImprimirMarcaAgua { get; set; }
    public bool SendEmail { get; set; }
    
    
    
}