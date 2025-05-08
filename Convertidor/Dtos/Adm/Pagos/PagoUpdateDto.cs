namespace Convertidor.Dtos.Adm.Pagos;

public class PagoUpdateDto
{
    public int CodigoLote { get; set; }
    public int CodigoCheque { get; set; }
    public int CodigoCuentaBanco { get; set; }
    public string NroCuenta { get; set; } = string.Empty;
    public int CodigoBanco { get; set; } 
    public int TipoChequeID { get; set; }
    public DateTime FechaPago { get; set; }
    public string FechaPagoString { get; set; }
    public FechaDto FechaPagoObj { get; set; }
    public int CodigoProveedor { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public int CodigoPresupuesto { get; set; }
    
    //Datos del Beneficiario
    public int CodigoBeneficiarioPago { get; set; }
    public int CodigoBeneficiarioOP { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoAnulado { get; set; }
}