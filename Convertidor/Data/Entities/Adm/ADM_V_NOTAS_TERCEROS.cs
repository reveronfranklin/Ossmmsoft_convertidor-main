namespace Convertidor.Data.Entities.Adm;

public class ADM_V_NOTAS_TERCEROS
{

    public int CODIGO_LOTE_PAGO { get; set; }
    public int CODIGO_CHEQUE { get; set; }
    public int NUMERO_CHEQUE { get; set; }
    public DateTime FECHA_CHEQUE { get; set; }
    public  string NOMBRE { get; set; }
    public  string NO_CUENTA { get; set; }
    public  string PAGAR_A_LA_ORDEN_DE { get; set; }
    public  string MOTIVO { get; set; }
    public  decimal MONTO { get; set; }
    public  string ENDOSO { get; set; }
    public  string USUARIO_INS { get; set; }  
    public int CODIGO_PROVEEDOR { get; set; }
    public  string DETALLE_OP_ICP_PUC { get; set; }  
    public  decimal MONTO_OP_ICP_PUC { get; set; }
    public  string DETALLE_IMP_RET { get; set; }  
    public  decimal MONTO_IMP_RET { get; set; }
    public int CODIGO_PRESUPUESTO { get; set; } 
    
    
}