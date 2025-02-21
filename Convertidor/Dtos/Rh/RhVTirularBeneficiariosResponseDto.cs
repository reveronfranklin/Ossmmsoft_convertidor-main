namespace Convertidor.Dtos.Rh;

public class RhVTitularBeneficiariosResponseDto
{
    
    public int Id { get; set; }
    public string? CedulaTitular { get; set; }
    public string? CedulaBeneficiario { get; set; }
    public string? NombreTituBene { get; set; }
    public string? ApellidosTituBene { get; set; }       
    public DateTime? FechaNacimientoFamiliar { get; set; } 
    public string? FechaNacimientoFamiliarString { get; set; } 
    public FechaDto? FechaNacimientoFamiliarObj { get; set; } 
    public string? Sexo { get; set; }
    public string? EstadoCivil { get; set; }
    public string? CdLocalidad { get; set; }
    public string? CdGrupo { get; set; }
    public string? CdBanco { get; set; }
    public string? NuCuenta { get; set; }
    public string? TpCuenta { get; set; }
    public string? DeEmail { get; set; }
    public string? NroArea { get; set; }
    public string? NroTelefono { get; set; } 
    public DateTime? FechaEgreso { get; set; }
    
    public int? CodigoIcp { get; set; }
    public string? Edad { get; set; }
    public string? TiempoServicio { get; set; }
    public string? Parentesco { get; set; }
    public string? Vinculo { get; set; }
    public string? TipoNomina { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string? FechaIngresoString { get; set; }
    public FechaDto? FechaIngresoObj { get; set; }
    public string? UnidadDescripcion { get; set; }
    public string? CargoNominal { get; set; }
    public string? AntiguedadCmc { get; set; }
    public string?   AntiguedadOtros { get; set; }
    
    public string SearchText { get { return $"{CedulaTitular}-{CedulaBeneficiario}-{NombreTituBene}-{ApellidosTituBene}-{UnidadDescripcion}-{CargoNominal}-{TipoNomina}"; } }

    
}