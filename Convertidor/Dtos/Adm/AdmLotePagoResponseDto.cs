namespace Convertidor.Dtos.Adm;

public class AdmLotePagoResponseDto
{
    public int CodigoLotePago { get;  set; }
    public int TipoPagoId { get;  set; }
    public string DescripcionTipoPago { get;  set; }
    public DateTime FechaPago { get;  set; }
    public string FechaPagoString { get;  set; }
    public FechaDto FechaPagoDto { get;  set; }
    public int CodigoCuentaBanco { get;  set; }
    public string NumeroCuenta { get;  set; }
    public int CodigoBanco { get;  set; }
    public string NombreBanco { get;  set; }
    public string SearchText { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public int CodigoPresupuesto { get; set; }
}