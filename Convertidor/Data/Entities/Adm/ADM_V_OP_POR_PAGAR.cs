namespace Convertidor.Data.Entities.Adm;

public class ADM_V_OP_POR_PAGAR
{
    public string  NUMERO_ORDEN_PAGO  { get; set; }
    public int  CODIGO_ORDEN_PAGO  { get; set; }
    public DateTime  FECHA_ORDEN_PAGO  { get; set; }   
    public string  TIPO_ORDEN_PAGO  { get; set; }   
    public string  NUMERO_RECIBO  { get; set; }     
    public int  CODIGO_PERIODICO_OP  { get; set; }
    public int  CODIGO_PRESUPUESTO  { get; set; }
    
}