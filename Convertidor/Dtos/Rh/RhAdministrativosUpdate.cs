namespace Convertidor.Dtos.Rh;

public class RhAdministrativosUpdate
{
    public int CodigoAdministrativo { get; set; }
    public int CodigoPersona { get; set; }
    public string FechaIngreso { get; set; } = string.Empty;
    public string TipoPago { get; set; } = string.Empty;
    public int BancoId { get; set; }
    public int TipoCuentaId { get; set; }
    public string NoCuenta { get; set; } = string.Empty;
}