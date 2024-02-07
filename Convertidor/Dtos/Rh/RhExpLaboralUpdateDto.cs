namespace Convertidor.Dtos.Rh;

public class RhExpLaboralUpdateDto
{
    public int CodigoExpLaboral { get; set; }
    public int CodigoPersona { get; set; }
    public string NombreEmpresa { get; set; } = string.Empty;
    public string TipoEmpresa { get; set; } = string.Empty;
    public int RamoId { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public DateTime FechaDesde { get; set; }
    public DateTime FechaHasta { get; set; }
    public int UltimoSueldo { get; set; }
    public string Supervisor { get; set; } = string.Empty;
    public string CargoSupervisor { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Extra1 { get; set; } = string.Empty;
    public string Extra2 { get; set; } = string.Empty;
    public string Extra3 { get; set; } = string.Empty;

}