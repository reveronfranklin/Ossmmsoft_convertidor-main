#nullable enable
namespace Convertidor.Data.Entities.Adm;

public class ADM_V_NOTAS
{
    public int CODIGO_LOTE_PAGO { get; set; }
    public int CODIGO_PAGO{ get; set; }
    public int NUMERO_PAGO { get; set; }
    public DateTime FECHA_PAGO { get; set; }
    public string? NOMBRE { get; set; }
    public string? NO_CUENTA { get; set; }
    public string? PAGAR_A_LA_ORDEN_DE { get; set; }
    public string MOTIVO { get; set; }
    public decimal MONTO { get; set; }
    public string? ENDOSO { get; set; }
    public int? USUARIO_INS { get; set; }  
    public int CODIGO_PROVEEDOR { get; set; }  
    public string? DETALLE_OP_ICP_PUC { get; set; }
    public decimal? MONTO_OP_ICP_PUC { get; set; } 
    public string? DETALLE_IMP_RET { get; set; }
    public decimal? MONTO_IMP_RET { get; set; }   
    public string? ETIQUETA2 { get; set; }
    public long? TIPO_PAGO_ID { get; set; }
    public string? TITULO_REPORTE { get; set; }
    



}