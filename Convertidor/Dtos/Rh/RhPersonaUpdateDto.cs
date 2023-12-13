namespace Convertidor.Dtos.Rh;

public class RhPersonaUpdateDto
{
    public int CodigoPersona { get; set; }
    public int Cedula { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Nacionalidad { get; set; } = string.Empty;
    public string Sexo { get; set; } = string.Empty;
    public int Edad { get; set; } 
    public string FechaNacimiento { get; set; } = string.Empty;
    public int PaisNacimientoId { get; set; }
    public int EstadoNacimientoId { get; set; }
   
    public string? NumeroGacetaNacional { get; set; } = string.Empty;
    
    public string? FechaGacetaNacional { get; set; } = string.Empty;
    public int EstadoCivilId { get; set; }
    public decimal Estatura { get; set; }
    public decimal Peso { get; set; }
    public string ManoHabil { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int IdentificacionId { get; set; }
    public int NumeroIdentificacion { get; set; }

}