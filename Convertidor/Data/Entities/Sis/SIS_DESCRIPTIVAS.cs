namespace Convertidor.Data.Entities.Sis;

public class SIS_DESCRIPTIVAS
{
    public int DESCRIPCION_ID { get; set; }
    public int DESCRIPCION_TITULO_ID { get; set; }
    public string DESCRIPCION { get; set; } = string.Empty;
    public string CODIGO_DESCRIPCION { get; set; } = string.Empty;
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; } = string.Empty;
    public int? USUARIO_INS { get; set; }
    public DateTime? FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int ESTATUS { get; set; }
}

