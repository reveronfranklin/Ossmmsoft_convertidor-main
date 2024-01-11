namespace Convertidor.Data.Entities.Rh;

public class RH_CONCEPTOS_ACUMULA
{
    
    public int CODIGO_CONCEPTO_ACUMULA { get; set; }
    public int CODIGO_CONCEPTO { get; set; }
    public string TIPO_ACUMULADO_ID { get; set; }=String.Empty;
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int CODIGO_CONCEPTO_ASOCIADO { get; set; }
    public DateTime FECHA_DESDE { get; set; }
    public DateTime FECHA_HASTA { get; set; }
    
}