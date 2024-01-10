namespace Convertidor.Dtos.Adm;

public class AdmProveedorActividadUpdateDto
{
    public int CodigoActProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public int ActividadId { get; set; }
    public DateTime FechaIni { get; set; } 
    public string FechaIniString { get; set; }  = string.Empty;
    public FechaDto FechaIniObj { get; set; } 
    public DateTime FechaFin { get; set; }
    public string FechaFinString { get; set; } = string.Empty;
    public FechaDto FechaFinObj { get; set; } 
}