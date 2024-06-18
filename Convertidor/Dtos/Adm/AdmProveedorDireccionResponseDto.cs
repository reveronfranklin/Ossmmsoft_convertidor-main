namespace Convertidor.Dtos.Adm;

public class AdmProveedorDireccionResponseDto
{
    public int CodigoDirProveedor  { get; set; } 
    public int CodigoProveedor  { get; set; } 
    public int TipoDireccionId  { get; set; } 
    public int PaisId  { get; set; } 
    public int EstadoId  { get; set; } 
    public int MunicipioId  { get; set; } 
    public int CiudadId  { get; set; } 
    public int ParroquiaId  { get; set; } 
    public int SectorId  { get; set; } 
    public int UrbanizacionId  { get; set; } 
    public int ManzanaId  { get; set; } 
    public int ParcelaId  { get; set; } 
    public int VialidadId  { get; set; } 
    public string Vialidad  { get; set; } = String.Empty;
    public int TipoViviendaId  { get; set; } 
    public string Vivienda  { get; set; } = String.Empty;
    public int TipoNivelId  { get; set; } 
    public string Nivel  { get; set; } = String.Empty;
    public int TipoUnidadId  { get; set; } 
    public string NumeroUnidad  { get; set; } = String.Empty;
    public string ComplementoDir  { get; set; } = String.Empty;   
    public int TenenciaId  { get; set; } 
    public int CodigoPostal  { get; set; } 
    public int Principal  { get; set; }    
}