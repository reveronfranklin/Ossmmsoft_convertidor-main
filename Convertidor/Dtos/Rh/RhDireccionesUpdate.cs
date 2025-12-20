namespace Convertidor.Dtos.Rh;

public class RhDireccionesUpdate
{
    public int CodigoDireccion { get; set; }
    public int CodigoPersona { get; set; }
    public int DireccionId { get; set; } 
    public int PaisId { get; set; } 
    public int EstadoId { get; set; }
    public int MunicipioId { get; set; }
    public int CiudadId { get; set; }
    public int ParroquiaId { get; set; }
   public int SectorId { get; set; }
    public int UrbanizacionId { get; set; }
    //public int ManzanaId { get; set; }
    //public int ParcelaId { get; set; }
    //public int VialidadId { get; set; }
    //public string Vialidad { get; set; } = string.Empty;
    public int TipoViviendaId { get; set; }
    public int ViviendaId { get; set; }
    public string Vivienda { get; set; } = string.Empty;
    public int TipoNivelId { get; set; }
    public string Nivel { get; set; } = string.Empty;
    public string NroVivienda { get; set; } = string.Empty;
    public string ComplementoDir { get; set; } = string.Empty;
    public int TenenciaId { get; set; }
    public int CodigoPostal { get; set; }
    public bool Principal { get; set; }
  
}