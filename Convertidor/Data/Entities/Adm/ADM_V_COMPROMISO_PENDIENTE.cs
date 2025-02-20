namespace Convertidor.Data.Entities.Adm;

public class ADM_V_COMPROMISO_PENDIENTE
{
    public int CODIGO_PRESUPUESTO { get; set; }
    public int CODIGO_IDENTIFICADOR { get; set; }
    public string NUMERO_IDENTIFICADOR { get; set; }
    public string ORDER_BY { get; set; }
    public string ORIGEN_COMPROMISO { get; set; }
    public DateTime FECHA_IDENTIFICADOR { get; set; }
    public string? CODIGO_VAL_CONTRATO { get; set; }
    public string? DESCRIPCION { get; set; }
    public int CODIGO_PROVEEDOR { get; set; }
    public string NOMBRE_PROVEEDOR { get; set; }  
    public string MOTIVO { get; set; }  
    public string ESTATUS_FISCO_VALOR { get; set; } 
    public decimal MONTO_POR_CAUSAR { get; set; }  
 
    
}