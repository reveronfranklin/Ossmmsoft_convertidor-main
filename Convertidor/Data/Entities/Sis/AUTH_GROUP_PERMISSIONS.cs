namespace Convertidor.Data.Entities.Sis;

public class AUTH_GROUP_PERMISSIONS
{
    public int ID { get; set; }  
    public int GROUP_ID { get; set; }  
    public int PERMISSION_ID { get; set; }  
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }

}