namespace Convertidor.Dtos.Bm;

public class Bm1Filter
{
    public DateTime FechaDesde { get; set; }
    public DateTime FechaHasta { get; set; }
    public List<ICPGetDto> ListIcpSeleccionado { get; set; }
    
}