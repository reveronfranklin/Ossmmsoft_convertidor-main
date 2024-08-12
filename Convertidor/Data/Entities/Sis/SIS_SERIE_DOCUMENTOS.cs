namespace Convertidor.Data.Entities.Sis;

public class SIS_SERIE_DOCUMENTOS
{

    public int CODIGO_SERIE_DOCUMENTO { get; set; }
    public int TIPO_DOCUMENTO_ID { get; set; }
    public string SERIE_LETRAS { get; set; }   
    public int NUMERO_SERIE_INICIAL { get; set; }
    public int NUMERO_SERIE_ACTUAL { get; set; }
    public int MAX_DIGITOS { get; set; }
    public string SERIE_COMPUESTA_INICIAL { get; set; }   
    public string SERIE_COMPUESTA_ACTUAL { get; set; }   
    public int CODIGO_APLICACION { get; set; }
    public int CODIGO_USUARIO { get; set; }
    public DateTime FECHA_VIGENCIA_INI { get; set; }
    public DateTime? FECHA_VIGENCIA_FIN { get; set; }
    public int SESION { get; set; }
    public string? EXTRA1 { get; set; } = string.Empty;
    public string? EXTRA2 { get; set; } = string.Empty;
    public string? EXTRA4 { get; set; } = string.Empty;
    public string? EXTRA5 { get; set; } = string.Empty;
    public string? EXTRA6 { get; set; } = string.Empty;
    public string? EXTRA7 { get; set; } = string.Empty;
    public string? EXTRA8 { get; set; } = string.Empty;
    public string? EXTRA9 { get; set; } = string.Empty;
    public string? EXTRA10 { get; set; } = string.Empty;
    public string? EXTRA11 { get; set; } = string.Empty;
    public string? EXTRA12 { get; set; } = string.Empty;
    public string? EXTRA13 { get; set; } = string.Empty;
    public string? EXTRA14 { get; set; } = string.Empty;
    public string? EXTRA15 { get; set; } = string.Empty;
    public int? USUARIO_INS { get; set; }
    public DateTime? FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int NUMERO_SERIE_FINAL { get; set; }
    public string SERIE_COMPUESTA_FINAL { get; set; } = string.Empty;
    public int CODIGO_PRESUPUESTO { get; set; }    
    
    
  
}