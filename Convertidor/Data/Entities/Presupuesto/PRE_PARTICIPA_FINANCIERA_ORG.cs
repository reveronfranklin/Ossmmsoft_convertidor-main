namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_PARTICIPA_FINANCIERA_ORG
    {
        public int CODIGO_PARTICIPA_FINANC_ORG { get; set; }
        public int ANO { get; set; }
        public int CODIGO_ORGANISMO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CUOTA_PARTICIPACION { get; set; }
        public string OBSERVACIONES { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PUC { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }

    }
}
