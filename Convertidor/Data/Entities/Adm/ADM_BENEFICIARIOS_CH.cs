namespace Convertidor.Data.Entities.Adm;

public class ADM_BENEFICIARIOS_CH
{
    public int CODIGO_BENEFICIARIO_CH { get; set; }
    public int CODIGO_CHEQUE { get; set; }
    public int CODIGO_BENEFICIARIO_OP { get; set; }
    public int CODIGO_PERIODICO_OP { get; set; }
    public int CODIGO_ORDEN_PAGO { get; set; }
    public string NUMERO_ORDEN_PAGO { get; set; }
    public decimal MONTO { get; set; }
    public decimal MONTO_ANULADO { get; set; }
    public string EXTRA1 { get; set; }
    public string EXTRA2 { get; set; }
    public string EXTRA3 { get; set; }
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int CODIGO_PRESUPUESTO { get; set; }
    
    
}