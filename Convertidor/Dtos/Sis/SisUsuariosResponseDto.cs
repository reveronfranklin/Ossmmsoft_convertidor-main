namespace Convertidor.Dtos.Sis;

public class SisUsuariosResponseDto
{
    public int CodigoUsuario { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public int? Cedula { get; set; }
    public int? DepartamentoId { get; set; }
    public string DescripcionDepartamento { get; set; } = string.Empty;
    public int? CargoId { get; set; }
    public string DescripcionCargo { get; set; } = string.Empty;
    public int? SistemaId { get; set; }
    public string DescripcionSistema { get; set; } = string.Empty;
    public DateTime? FechaIngreso { get; set; }
    public string FechaIngresoString { get; set; }
    public FechaDto FechaIngresoObj { get; set; }
    public DateTime? FechaEgreso { get; set; }
    public string FechaEgresoString { get; set; }
    public FechaDto FechaEgresoObj { get; set; }
    public int? Prioridad { get; set; }
    public string? DescripcionPrioridad { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public string? DescripcionStatus { get; set; } = string.Empty;
    public bool IsSuperUser { get; set; }
}