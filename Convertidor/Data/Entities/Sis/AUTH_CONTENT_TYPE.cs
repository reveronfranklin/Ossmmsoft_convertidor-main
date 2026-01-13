namespace Convertidor.Data.Entities.Sis;

public class AUTH_CONTENT_TYPE
{
    public int ID { get; set; }  
    public string APP_LABEL { get; set; }  
    public string MODEL { get; set; }  
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    
}