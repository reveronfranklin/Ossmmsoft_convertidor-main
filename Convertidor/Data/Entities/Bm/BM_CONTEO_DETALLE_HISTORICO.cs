namespace Convertidor.Data.Entities.Bm;

public class BM_CONTEO_DETALLE_HISTORICO
{
   
    public int CODIGO_BM_CONTEO { get; set; }
    public int CONTEO { get; set; }
    public int CODIGO_ICP { get; set; }
    public string UNIDAD_TRABAJO { get; set; } = string.Empty;
    public string CODIGO_GRUPO { get; set; } = string.Empty;
    public string CODIGO_NIVEL1 { get; set; } = string.Empty;
    public string CODIGO_NIVEL2 { get; set; } = string.Empty;
    public string NUMERO_LOTE { get; set; } = string.Empty;
    public int CANTIDAD { get; set; }
    public int CANTIDAD_CONTADA { get; set; }
    public int DIFERENCIA { get; set; }
    public string NUMERO_PLACA { get; set; } = string.Empty;
    public decimal VALOR_ACTUAL { get; set; } 
    public string ARTICULO { get; set; } = string.Empty;
    public string ESPECIFICACION { get; set; } = string.Empty;
    public string SERVICIO { get; set; } = string.Empty;
    public string RESPONSABLE_BIEN { get; set; } = string.Empty;
    public DateTime FECHA_MOVIMIENTO { get; set; }
    public int CODIGO_BIEN { get; set; }
    public int CODIGO_MOV_BIEN { get; set; }
    public string COMENTARIO { get; set; } = string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int USUARIO_UPD { get; set; }
    public DateTime FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int REPLICAR_COMENTARIO { get; set; }
    public int CODIGO_ICP_FISICO { get; set; }
}