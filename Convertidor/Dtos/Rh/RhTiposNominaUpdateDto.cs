namespace Convertidor.Dtos.Rh;

public class RhTiposNominaUpdateDto
{
    public int CodigoTipoNomina { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string SiglasTipoNomina { get; set; } = string.Empty;
    public int FrecuenciaPagoId { get; set; }
    public string Extra1 { get; set; } = string.Empty;
    public string Extra2 { get; set; } = string.Empty;
    public string Extra3 { get; set; } = string.Empty;
    public int UsuarioUpd { get; set; }
    public DateTime FechaUpd { get; set; }
    public int CodigoEmpresa { get; set; }
    public decimal SueldoMinimo { get; set; }
    public int CodigoPresupuestoActual { get; set; }
}