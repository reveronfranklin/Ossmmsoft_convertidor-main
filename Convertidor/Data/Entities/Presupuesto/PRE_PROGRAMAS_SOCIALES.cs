namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_PROGRAMAS_SOCIALES
    {
        public int CODIGO_PRG_SOCIAL { get; set; }
        public int CODIGO_ICP { get; set; }
        public string DENOMINACION { get; set; }=string.Empty;
        public int ORGANISMO_ID { get; set; }
        public int ASIGNACION_ANUAL { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int ANO { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }

    }
}
