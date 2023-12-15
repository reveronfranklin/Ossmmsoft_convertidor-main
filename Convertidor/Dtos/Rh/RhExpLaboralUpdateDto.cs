namespace Convertidor.Dtos.Rh;

public class RhExpLaboralUpdateDto
{
    public int CodigoExpLaboral { get; set; }
    public int CodigoPersona { get; set; }
    public string NombreEmpresa { get; set; }
    public string TipoEmpresa { get; set; }
    public int RamoId { get; set; }
    public string Cargo { get; set; }
    public DateTime FechaDesde { get; set; }
    public DateTime FechaHasta { get; set; }
    public int UltimoSueldo { get; set; }
    public string Supervisor { get; set; }
    public string CargoSupervisor { get; set; }
    public string Telefono { get; set; }
    public string Descripcion { get; set; }
  




}