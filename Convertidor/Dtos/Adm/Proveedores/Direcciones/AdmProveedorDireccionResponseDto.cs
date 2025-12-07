namespace Convertidor.Dtos.Adm.Proveedores.Direcciones;

public class AdmProveedorDireccionResponseDto
{
    public int CodigoDirProveedor  { get; set; } 
    public int CodigoProveedor  { get; set;}
   public int TipoDireccionId  { get; set; } 
    public string TipoDireccion  { get; set; }


    public int PaisId { get; set; }
    public string Pais { get; set; }
    public int EstadoId { get; set; }
    public string Estado { get; set; }
    public int MunicipioId { get; set; }
    public string Municipio { get; set; }
    public int CiudadId { get; set; }
    public string Ciudad { get; set; }
    public int ParroquiaId { get; set; }
    public string Parroquia { get; set; }
    public int SectorId { get; set; }
    public string Sector { get; set; }
    public int UrbanizacionId { get; set; }
    public string Urbanizacion { get; set; }

    public int TipoViviendaId { get; set; }
    public string TipoVivienda { get; set; } = string.Empty;

    public string Vivienda { get; set; } = string.Empty;
    public int TipoNivelId { get; set; }
    public string TipoNivel { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public string NroUnidad { get; set; } = string.Empty;
    public string ComplementoDir { get; set; } = string.Empty;
    public int TenenciaId { get; set; }
    public string Tenencia { get; set; } = string.Empty;
    public int CodigoPostal { get; set; }
    public bool Principal { get; set; }

}