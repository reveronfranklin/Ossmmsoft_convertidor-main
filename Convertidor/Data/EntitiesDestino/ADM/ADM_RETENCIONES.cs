namespace Convertidor.Data.EntitiesDestino.ADM;

public class ADM_RETENCIONES
{
   
        public int CODIGO_RETENCION { get; set; }
        public int TIPO_RETENCION_ID { get; set; }
        public string? CONCEPTO_PAGO { get; set; }=string.Empty;
        public int? TIPO_PERSONA_ID { get; set; }
        public decimal? BASE_IMPONIBLE { get; set; }
        public decimal? POR_RETENCION { get; set; }
        public decimal? MONTO_RETENCION { get; set; }
        public DateTime? FECHA_INI { get; set; }
        public DateTime? FECHA_FIN { get; set; }
        public string? EXTRA1 { get; set; }=string.Empty;
        public string? EXTRA2 { get; set; }= string.Empty;
        public string? EXTRA3 { get; set; } = string.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int? CODIGO_EMPRESA { get; set; }


}