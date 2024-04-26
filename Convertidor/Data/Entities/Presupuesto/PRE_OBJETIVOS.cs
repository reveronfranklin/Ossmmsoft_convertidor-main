namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_OBJETIVOS
    {
        public int CODIGO_OBJETIVO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int NUMERO_OBJETIVO { get; set; }
        public string DENOMINACION_OBJETIVO { get; set; } = string.Empty;
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
