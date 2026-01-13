namespace Convertidor.Data.Entities.Sis;

public class AUTH_USER
{
    public int ID { get; set; }  
    public string PASSWORD { get; set; }  
    public string LAST_LOGIN { get; set; }  
    public int IS_SUPERUSER { get; set; }  
    public string USERNAME { get; set; }  
    public string LAST_NAME { get; set; }  
    public string FIRST_NAME { get; set; }  
    public string EMAIL { get; set; }  
    public int IS_STAFF { get; set; }  
    public int IS_ACTIVE { get; set; }  
    public DateTime DATE_JOINED { get; set; }
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
}


    

    
