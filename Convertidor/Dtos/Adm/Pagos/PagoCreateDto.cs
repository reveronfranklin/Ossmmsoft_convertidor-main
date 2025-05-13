namespace Convertidor.Dtos.Adm.Pagos;

public class PagoCreateDto
{
    public int CodigoLote { get; set; }
    public int CodigoProveedor { get; set; }
    public int CodigoCuentaBanco { get; set; }
    public string NroCuenta { get; set; } = string.Empty;
    public int CodigoBanco { get; set; } 
    public int TipoChequeID { get; set; }
    public DateTime FechaPago { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public int CodigoPresupuesto { get; set; }
    
    //Datos del Beneficiario
    public int CodigoBeneficiarioOP { get; set; }
    public int CodigoOrdenPago { get; set; }
    public string NumeroOrdenPago { get; set; }
    public decimal Monto { get; set; }

}