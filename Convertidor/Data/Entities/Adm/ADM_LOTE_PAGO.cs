namespace Convertidor.Data.Entities.Adm;

public class ADM_LOTE_PAGO
{
    public int CODIGO_LOTE_PAGO { get;  set; }
    public int TIPO_PAGO_ID { get;  set; }
    public DateTime FECHA_PAGO { get;  set; }
    public int CODIGO_CUENTA_BANCO { get;  set; }
    
    public string? STATUS { get; set; } = string.Empty;
    public string? TITULO { get; set; } = string.Empty;
    public string? SEARCH_TEXT { get; set; } = string.Empty;
    public int? USUARIO_INS { get; set; }
    public DateTime? FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int? CODIGO_EMPRESA { get; set; }
    public int? CODIGO_PRESUPUESTO { get; set; }

    
}