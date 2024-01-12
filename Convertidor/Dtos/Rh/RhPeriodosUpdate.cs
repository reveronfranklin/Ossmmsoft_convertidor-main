namespace Convertidor.Dtos.Rh;

public class RhPeriodosUpdate
{
    public int CodigoPeriodo { get; set; }
    public int CodigoTipoNomina { get; set; }
    public DateTime FechaNomina { get; set; }
    public int Periodo { get; set; }
    public string TipoNomina { get; set; }
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int UsuarioIns { get; set; }
    public DateTime FechaIns { get; set; }
    public int UsuarioUpd { get; set; }
    public DateTime FechaUpd { get; set; }
    public int UsuarioPreCierre { get; set; }
    public DateTime FechaPreCierre { get; set; }
    public int UsuarioCierre { get; set; }
    public DateTime FechaCierre { get; set; }
    public int CodigoEmpresa { get; set; }
    public int CodigoCuentaEmpresa { get; set; }
    public int UsuarioPreNomina { get; set; }
    public DateTime FechaPrenomina { get; set; }
    public int CodigoPresupuesto { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    
    public bool UpdatePreNomina { get; set; }
    public bool UpdatePreCierre { get; set; }
    public bool UpdateCierre { get; set; }
    
}