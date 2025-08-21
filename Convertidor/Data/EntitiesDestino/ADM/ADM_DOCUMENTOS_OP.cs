namespace Convertidor.Data.EntitiesDestino.ADM;

public class ADM_DOCUMENTOS_OP
{
    public int CODIGO_DOCUMENTO_OP { get; set; }
    public int CODIGO_ORDEN_PAGO { get; set; }
    public DateTime FECHA_COMPROBANTE { get; set; }
    public string PERIODO_IMPOSITIVO { get; set; } = string.Empty;
    public int TIPO_OPERACION_ID { get; set; }
    public int TIPO_DOCUMENTO_ID { get; set; }
    public DateTime FECHA_DOCUMENTO { get; set; }
    public string? NUMERO_DOCUMENTO { get; set; } = string.Empty;
    public string NUMERO_CONTROL_DOCUMENTO { get; set; } = string.Empty;
    public decimal MONTO_DOCUMENTO { get; set; }
    public decimal BASE_IMPONIBLE { get; set; }
    public decimal MONTO_IMPUESTO { get; set; }
    public string? NUMERO_DOCUMENTO_AFECTADO { get; set; } = string.Empty;
    public int TIPO_TRANSACCION_ID { get; set; }
    public int TIPO_IMPUESTO_ID { get; set; }
    public decimal MONTO_IMPUESTO_EXENTO { get; set; }
    public decimal MONTO_RETENIDO { get; set; }
    public string? EXTRA1 { get; set; } = string.Empty;
    public string? EXTRA2 { get; set; } = string.Empty;
    public string? EXTRA3 { get; set; }= string.Empty;
    public int USUARIO_INS { get; set; }
    public DateTime FECHA_INS { get; set; }
    public int? USUARIO_UPD { get; set; }
    public DateTime? FECHA_UPD { get; set; }
    public int CODIGO_EMPRESA { get; set; }
    public int CODIGO_PRESUPUESTO { get; set; }
    public string? NUMERO_EXPEDIENTE { get; set; }=string.Empty;
    public int? ESTATUS_FISCO_ID { get; set; }
    public string? NUMERO_COMPROBANTE { get; set; }=string.Empty;
}