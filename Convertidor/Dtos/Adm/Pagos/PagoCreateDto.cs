namespace Convertidor.Dtos.Adm.Pagos;

public class PagoCreateDto
{
    
    //Datos del Pago
    public int CodigoLote { get; set; } //Indicado Por el usuario segun el lote donde se encuentre el pago
    public string Motivo { get; set; } = string.Empty; //Indicado por el usuario
  
    
    //Datos del Beneficiario
    public int CodigoOrdenPago { get; set; }  //indicado por el usuario segun la vista===> ADM_V_OP_POR_PAGAR
    public string NumeroOrdenPago { get; set; }  //indicado por el usuario segun la vista===> ADM_V_OP_POR_PAGAR

    public int CodigoBeneficiarioOP { get; set; } //Ingresado por el usuario segun la vista ===>ADM_V_OP_POR_PAGAR_BENE
    
    public decimal Monto { get; set; } //Ingresado por el usuario segun la vista ===>ADM_V_OP_POR_PAGAR_BENE
        
  

} 