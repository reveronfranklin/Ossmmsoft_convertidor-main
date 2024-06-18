namespace Convertidor.Dtos.Rh;

public class RhHPeriodosUpdate
{
    public int CodigoHPeriodo { get; set; }
    public int CodigoPeriodo { get; set; }
    public DateTime FechaInsH { get; set; }
    public int UsuarioInsH { get; set; }
    public int CodigoTipoNomina { get; set; }
    public DateTime FechaNomina { get; set; }
    public int Periodo { get; set; }
    public string TipoNomina { get; set; }
    public string Extra1 { get; set; } = string.Empty;
    public string Extra2 { get; set; } = string.Empty;
    public string Extra3 { get; set; } = string.Empty; 
    public int UsuarioPreCierre { get; set; }
    public DateTime FechaPreCierre { get; set; }
    public int UsuarioCierre { get; set; }
    public DateTime FechaCierre { get; set; }
    public int CodigoCuentaEmpresa { get; set; }
    public int UsuarioPreNomina { get; set; }
    public DateTime FechaPrenomina { get; set; }
    public int CodigoPresupuesto { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}