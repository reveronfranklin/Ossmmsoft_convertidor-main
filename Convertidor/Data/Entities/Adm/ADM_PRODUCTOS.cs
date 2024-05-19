namespace Convertidor.Data.Entities.Adm;

public class ADM_PRODUCTOS
{
    public int CODIGO_PRODUCTO  { get; set; }	
    public string CODIGO { get; set; } = string.Empty;
    public string CODIGO_PRODUCTO1 { get; set; } = string.Empty;
    public string CODIGO_PRODUCTO2 { get; set; } = string.Empty;
    public string CODIGO_PRODUCTO3 { get; set; } = string.Empty;
    public string CODIGO_PRODUCTO4 { get; set; } = string.Empty;
    public string DESCRIPCION { get; set; }=string.Empty;
    public int DESCRIPCION_ID { get; set; }
    public int DESCRIPCION_FK_ID { get; set; }
    public int TITULO_ID { get; set; }
    public string EXTRA1 { get; set; } = string.Empty;
    public string EXTRA2 { get; set; } = string.Empty;
    public string EXTRA3 { get; set; }=string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public string DESCRIPCION_REAL { get; set; }=string.Empty;
    public string CODIGO_REAL { get; set; }=string.Empty;
    public string CODIGO_PARENT { get; set; }=string.Empty;
    public int TIPO_PRODUCTO_ID { get; set; }
}