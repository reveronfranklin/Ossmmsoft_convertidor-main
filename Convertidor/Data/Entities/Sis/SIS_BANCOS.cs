namespace Convertidor.Data.Entities.Sis;

public class SIS_BANCOS
{
    public int CODIGO_BANCO { get; set; }
    public string NOMBRE { get; set; } = string.Empty;
    public string CODIGO_INTERBANCARIO { get; set; } = string.Empty;
    
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
}