namespace Convertidor.Data.Entities.Presupuesto;

public class PRE_IDENTIFICACIONES
{
    
    
    public int CODIGO_IDENTIFICACION { get; set; }
    public string PAIS { get; set; } = string.Empty;
    public string ESTADO { get; set; } = string.Empty;
    public string CIUDAD { get; set; } = string.Empty;
    public string MUNICIPIO { get; set; } = string.Empty;
    public string DOMICILIO_LEGAL { get; set; } = string.Empty;
    public string CODIGO_POSTAL { get; set; } = string.Empty;
    public string TELEFONO1 { get; set; } = string.Empty;
    public string TELEFONO2 { get; set; } = string.Empty;
    public string TELEFONO3 { get; set; } = string.Empty;
    public string FAX1 { get; set; } = string.Empty;
    public string FAX2 { get; set; } = string.Empty;
    public string FAX3 { get; set; } = string.Empty;
    public string EMAIL1 { get; set; } = string.Empty;
    public string EMAIL2 { get; set; } = string.Empty;
    public string EMAIL3 { get; set; } = string.Empty;
    public DateTime FECHA_CREACION { get; set; }
    public int CODIGO_PRESUPUESTO { get; set; }   
    public string DENOMINACION_ONP { get; set; } = string.Empty;
    public string DENOMINACION_OMP { get; set; } = string.Empty;
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    
}