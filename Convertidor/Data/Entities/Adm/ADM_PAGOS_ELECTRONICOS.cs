namespace Convertidor.Data.Entities.Adm;

public class ADM_PAGOS_ELECTRONICOS
{
    
    public int CODIGO_PAGO_ELECTRONICO { get; set; }
    public int CODIGO_LOTE { get; set; }
    public int CODIGO_PAGO { get; set; }
    public int CODIGO_BENEFICIARIO_PAGO { get; set; }
    public string DATA { get; set; }
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }   
    public int CODIGO_PRESUPUESTO { get; set; }

}