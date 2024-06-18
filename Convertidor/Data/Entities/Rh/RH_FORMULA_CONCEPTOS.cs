namespace Convertidor.Data.Entities.Rh;

public class RH_FORMULA_CONCEPTOS
{
    
    public int CODIGO_FORMULA_CONCEPTO { get; set; }
    public int CODIGO_CONCEPTO { get; set; }
    public decimal PORCENTAJE { get; set; }
    public decimal MONTO_TOPE { get; set; }
    public string TIPO_SUELDO { get; set; } = string.Empty;
    public DateTime? FECHA_DESDE { get; set; }
    public DateTime? FECHA_HASTA { get; set; }
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public decimal PORCENTAJE_PATRONAL { get; set; }
    
    
}