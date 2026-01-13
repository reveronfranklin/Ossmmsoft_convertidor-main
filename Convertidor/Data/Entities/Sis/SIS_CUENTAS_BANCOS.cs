namespace Convertidor.Data.Entities.Sis;

public class SIS_CUENTAS_BANCOS
{
    public int CODIGO_CUENTA_BANCO { get; set; }
    
    public int CODIGO_BANCO { get; set; }
    
    public int TIPO_CUENTA_ID { get; set; }
    
    public string NO_CUENTA { get; set; } = string.Empty;
    
    public string FORMATO_MASCARA { get; set; } = string.Empty;
    
    public int DENOMINACION_FUNCIONAL_ID { get; set; }
    
    public string CODIGO { get; set; } = string.Empty;
    
    public int PRINCIPAL { get; set; }
    
    public int RECAUDADORA { get; set; }
    
    public int CODIGO_MAYOR { get; set; }
    
    public int CODIGO_AUXILIAR { get; set; }
    
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    
    public string SEARCH_TEXT { get; set; } = string.Empty;
}