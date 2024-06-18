namespace Convertidor.Dtos.Rh;

public class RhFamiliarResponseDto
{
    public int CodigoPersona { get; set; }
    public int	CodigoFamiliar { get; set; }
    public int	 CedulaFamiliar { get; set; }
    public string Nacionalidad { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string Edad { get; set; } = string.Empty;
    public FechaDto FechaNacimientoObj { get; set; }
    public string FechaNacimientoString { get; set; }
    public int	ParienteId { get; set; }
    public string ParienteDescripcion { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public int	NivelEducativo { get; set; }
    public int	Grado { get; set; }
}