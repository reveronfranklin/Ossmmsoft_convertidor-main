namespace Convertidor.Data.Entities
{
    public class RH_HISTORICO_NOMINA
    {
        public long CODIGO_HISTORICO_NOMINA { get; set; }

        public DateTime FECHA_NOMINA { get; set; }

        public long CODIGO_PERIODO { get; set; }

        public long CODIGO_TIPO_NOMINA { get; set; }

        public long CODIGO_PERSONA { get; set; }

        public long CODIGO_CONCEPTO { get; set; }

        public string COMPLEMENTO_CONCEPTO { get; set; }

        public string TIPO { get; set; }

        public long FRECUENCIA_ID { get; set; }

        public decimal MONTO { get; set; }

        public string STATUS { get; set; }

        public string EXTRA1 { get; set; }

        public string EXTRA2 { get; set; }

        public string EXTRA3 { get; set; }

        public long USUARIO_INS { get; set; }

        public DateTime FECHA_INS { get; set; }

        public long USUARIO_UPD { get; set; }

        public DateTime FECHA_UPD { get; set; }

        public long CODIGO_EMPRESA { get; set; }
    }
}
