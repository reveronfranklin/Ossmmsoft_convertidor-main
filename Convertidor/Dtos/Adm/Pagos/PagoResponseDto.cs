namespace Convertidor.Dtos.Adm.Pagos;

public class PagoResponseDto
{
    public int CodigoLote { get; set; }
    public int CodigoPago { get; set; }
    public int CodigoCuentaBanco { get; set; }
    public string NroCuenta { get; set; } = string.Empty;
    public int CodigoBanco { get; set; } 
    public string NombreBanco { get; set; } = string.Empty;
    public int TipoChequeID { get; set; }
    public string DescripcionTipoCheque { get; set; } = string.Empty;
    public DateTime FechaPago { get; set; }
    public string FechaPagoString { get; set; }
    public FechaDto FechaPagoObj { get; set; }
    public int CodigoProveedor { get; set; }
    public string NombreProveedor { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? FechaEntrega { get; set; }
    public string? FechaEntregaString { get; set; }
    public FechaDto? FechaEntregaObj { get; set; }
    public int UsuarioEntrega { get; set; }
    public int CodigoPresupuesto { get; set; }
    
    //Datos del Beneficiario
    public int CodigoBeneficiarioPago { get; set; }
    public int CodigoBeneficiarioOP { get; set; }
    public int CodigoOrdenPago { get; set; }
    public string NumeroOrdenPago { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoAnulado { get; set; }
    public string NumeroCuentaProveedor { get; set; }
    
    
}