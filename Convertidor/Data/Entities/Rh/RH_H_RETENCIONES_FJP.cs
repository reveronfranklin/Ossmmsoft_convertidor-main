namespace Convertidor.Data.Entities.Rh
{
    public class RH_H_RETENCIONES_FJP
    {
        public int CODIGO_RETENCION_APORTE { get; set; }
        public int SECUENCIA { get; set; }
        public string UNIDAD_EJECUTORA { get; set; }
        public string CEDULATEXTO { get; set; }
        public string NOMBRES_APELLIDOS { get; set; }
        public string DESCRIPCION_CARGO { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public int MONTO_FJP_TRABAJADOR { get; set; }

        public int MONTO_FJP_PATRONO { get; set; }

        public int MONTO_TOTAL_RETENCION { get; set; }

        public string FECHA_NOMINA { get; set; }

        public string SIGLAS_TIPO_NOMINA { get; set; }

        public int PROCESO_ID { get; set; }

        public DateTime FECHA_DESDE { get; set; }

        public DateTime FECHA_HASTA { get; set; }

        public int CODIGO_TIPO_NOMINA { get; set; }
    }
}
