namespace Convertidor.Data.Entities.Sis;

public class SIS_EMPRESAS
{
    public int CODIGO_EMPRESA { get; set; }
    public string NOMBRE_EMPRESA { get; set; } = string.Empty;
    public int TIPO_EMPRESA_ID { get; set; }
    public int IDENTIFICACION_ID { get; set; }
    public string NUMERO_IDENTIFICACION { get; set; } = string.Empty;  
    public DateTime FECHA_INSTALACION { get; set; }
    public int VERSION_SISTEMA_ID { get; set; }   
    public int MARCA_SERVIDOR_ID { get; set; } 
    public int SISTEMA_OPERATIVO_ID { get; set; } 
    public string NOMBRE_SERVIDOR { get; set; } = string.Empty;    
    public string UNIDAD_INSTALACION { get; set; } = string.Empty;    
    public string DIRECTORIO_PRINCIPAL { get; set; } = string.Empty;    
    public string DIRECTORIO_PRIVADO { get; set; } = string.Empty;     
    public string DIRECTORIO_PUBLICO { get; set; } = string.Empty;  
    public string EXTRA1 { get; set; } = string.Empty;    
    public string EXTRA2 { get; set; } = string.Empty; 
    public string EXTRA3 { get; set; } = string.Empty; 
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public string CODIGO_ESTADO_CTA_FISCAL { get; set; } = string.Empty; 
    public string CODIGO_MUNICIPIO_CTA_FISCAL { get; set; } = string.Empty; 
    public string SEPARATOR { get; set; } = string.Empty; 
    public int GERENCIAL { get; set; } 
    public string EXTRA4 { get; set; } = string.Empty; 
    public string EXTRA5 { get; set; } = string.Empty; 
    public string EXTRA6 { get; set; } = string.Empty; 
    public string EXTRA7 { get; set; } = string.Empty; 
    public string EXTRA8 { get; set; } = string.Empty; 
    public string EXTRA9 { get; set; } = string.Empty; 
    public string EXTRA10 { get; set; } = string.Empty; 
    public string EXTRA11 { get; set; } = string.Empty; 
    public string EXTRA12 { get; set; } = string.Empty; 
    public string EXTRA13 { get; set; } = string.Empty; 
    public string EXTRA14 { get; set; } = string.Empty; 
    public string EXTRA15 { get; set; } = string.Empty; 
    public string DONDE_ESTOY { get; set; } = string.Empty; 
        
    
   
        
}