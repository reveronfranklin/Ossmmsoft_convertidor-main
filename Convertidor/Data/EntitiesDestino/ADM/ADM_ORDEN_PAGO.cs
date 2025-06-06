﻿namespace Convertidor.Data.EntitiesDestino.ADM
{
    public class ADM_ORDEN_PAGO
    {
        public int CODIGO_ORDEN_PAGO { get; set; }
        public int? ANO { get; set; }
        public int? CODIGO_COMPROMISO { get; set; }
        public int? CODIGO_ORDEN_COMPRA { get; set; }
        public int? CODIGO_CONTRATO { get; set; }
        public int CODIGO_PROVEEDOR { get; set; }
        public string NUMERO_ORDEN_PAGO { get; set; } = string.Empty;
        public string REFERENCIA_ORDEN_PAGO { get; set; } = string.Empty;
        public DateTime FECHA_ORDEN_PAGO { get; set; }
        public int TIPO_ORDEN_PAGO_ID { get; set; }
        public DateTime FECHA_PLAZO_DESDE { get; set; }
        public DateTime FECHA_PLAZO_HASTA { get; set; }
        public int? CANTIDAD_PAGO { get; set; }
        public int? NUMERO_PAGO { get; set; }
        public int? FRECUENCIA_PAGO_ID { get; set; }
        public int? TIPO_PAGO_ID { get; set; }
        public int? NUMERO_VALUACION { get; set; }
        public string? STATUS { get; set; }=string.Empty;
        public string? MOTIVO { get; set; }= string.Empty;
        public string? EXTRA1 { get; set; } = string.Empty;
        public string? EXTRA2 { get; set; } = string.Empty;
        public string? EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
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
        public decimal? NUMERO_COMPROBANTE { get; set; }
        public DateTime? FECHA_COMPROBANTE { get; set; }
        public decimal? NUMERO_COMPROBANTE2 { get; set; }
        public decimal? NUMERO_COMPROBANTE3 { get; set; }
        public decimal? NUMERO_COMPROBANTE4 { get; set; }
        public string? SEARCH_TEXT { get; set; } = string.Empty;
        
        public string? MONTO_LETRAS { get; set; }= string.Empty;
        public int? CON_FACTURA { get; set; }
        
        public string? TITULO_REPORTE { get; set; } = string.Empty;
        
        public string? DIRECCION_AGENTE_RETENCION { get; set; }= string.Empty;
        public string? NOMBRE_AGENTE_RETENCION { get; set; }= string.Empty;
        public string? RIF_AGENTE_RETENCION { get; set; }= string.Empty;
        public string? TELEFONO_AGENTE_RETENCION { get; set; }= string.Empty;
        public string? NUMERO_COMPROMISO { get; set; }= string.Empty;
        public DateTime FECHA_COMPROMISO { get; set; }

    }
}
