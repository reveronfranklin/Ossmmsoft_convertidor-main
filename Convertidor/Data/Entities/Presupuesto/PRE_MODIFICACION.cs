namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_MODIFICACION
    {
        public int CODIGO_MODIFICACION { get; set; }
        public int CODIGO_SOL_MODIFICACION { get; set; }
        public int TIPO_MODIFICACION_ID { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
        public int ANO { get; set; }
        public string NUMERO_MODIFICACION { get; set; } = string.Empty;
        public string NO_RES_ACT { get; set; } = string.Empty;
        public string CODIGO_OFICIO { get; set; } = string.Empty;
        public int CODIGO_SOLICITANTE { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
    }
}
