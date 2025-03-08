namespace Convertidor.Data.Entities.Rh;

public class RH_DOCUMENTOS_ADJUNTOS
{
    
    public int CODIGO_DOCUMENTO_ADJUNTO { get; set; }
    public int CODIGO_DOCUMENTO { get; set; }
    public string ADJUNTO { get; set; }
    public string TITULO { get; set; }
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; } 
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }

}