namespace Convertidor.Dtos.Presupuesto;

public class PreAsignacionesExcel
{
    public int CodigoPresupuesto { get; set; }
    public List<PreAsignacionesGetDto> Asignaciones  { get; set; }
     
}