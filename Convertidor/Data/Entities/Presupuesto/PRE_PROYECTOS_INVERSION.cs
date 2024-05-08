namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_PROYECTOS_INVERSION
    {
        public int CODIGO_PROYECTO_INV { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int FINANCIADO_ID { get; set; }
        public string CODIGO_OBRA { get; set; }= string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public int CODIGO_FUNCIONARIO { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public int SITUACION_ID { get; set; }
        public int COSTO { get; set; }
        public int COMPROMISO_ANTERIOR { get; set; }
        public int COMPROMISO_VIGENTE { get; set; }
        public int EJECUTADO_ANTERIOR { get; set; }
        public int EJECUTADO_VIGENTE { get; set; }
        public int ESTIMADA_PRESUPUESTO { get; set; }
        public int ESTIMADA_POSTERIOR { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public string FUNCIONARIO { get; set; }= string.Empty;
    }
}
