namespace Convertidor.Dtos.Sis;

public class SisUsuariosUpdateDto
{
    public int CodigoUsuario { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public int? Cedula { get; set; }
    public int? DepartamentoId { get; set; }
    public int? CargoId { get; set; }
    public int? SistemaId { get; set; }
   
    public DateTime? FechaIngreso { get; set; }
  
    public DateTime? FechaEgreso { get; set; }
  
    public int? Prioridad { get; set; }
    public string? Status { get; set; } = string.Empty;
    public bool IsSuperUser { get; set; }
}