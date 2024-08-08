namespace Convertidor.Data.Entities.Sis;

public class AUTH_PERMISSION
{
    public int ID { get; set; }  
    public int CONTENT_TYPE_ID { get; set; }  
    public string CODENAME { get; set; }  
    public string NAME { get; set; }  
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
}