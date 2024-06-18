namespace Convertidor.Data.Entities.Bm;

public class BM_CONTEO
{
    public int CODIGO_BM_CONTEO { get; set; } 
    public string TITULO { get; set; } = string.Empty;
    public string COMENTARIO { get; set; } = string.Empty;
    public int CODIGO_PERSONA_RESPONSABLE { get; set; } 
    public int CANTIDAD_CONTEOS_ID { get; set; } 
    public DateTime FECHA { get; set; } 
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
}