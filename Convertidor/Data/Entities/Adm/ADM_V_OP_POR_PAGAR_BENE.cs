namespace Convertidor.Data.Entities.Adm;

public class ADM_V_OP_POR_PAGAR_BENE
{
    public int  CODIGO_ORDEN_PAGO  { get; set; }
    public int  CODIGO_PRESUPUESTO  { get; set; }
    public int  CODIGO_PERIODICO_OP  { get; set; }
    public int  CODIGO_PROVEEDOR  { get; set; }
    public string  NUMERO_ORDEN_PAGO  { get; set; }
    public int  CODIGO_CONTACTO_PROVEEDOR  { get; set; }
    
    public string  PAGAR_A_LA_ORDEN_DE  { get; set; }  
    
    public string  NOMBRE_PROVEEDOR  { get; set; } 
    
    public decimal  MONTO_POR_PAGAR  { get; set; }
    
    public int  CODIGO_BENEFICIARIO_OP  { get; set; }
    
    public string  MOTIVO  { get; set; }   
    public decimal  MONTO_A_PAGAR  { get; set; }
    
  
  
  
  
 


}