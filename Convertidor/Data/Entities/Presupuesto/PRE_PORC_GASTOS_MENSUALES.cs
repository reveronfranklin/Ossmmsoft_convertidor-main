namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_PORC_GASTOS_MENSUALES
    {
        public int CODIGO_POR_GASTOS_MES { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_PUC { get; set; }
        public int POR_1_MES { get; set; }
        public int POR_2_MES { get; set; }
        public int POR_3_MES { get; set; }
        public int POR_4_MES { get; set; }
        public int POR_5_MES { get; set; }
        public int POR_6_MES { get; set; }
        public int POR_7_MES { get; set; }
        public int POR_8_MES { get; set; }
        public int POR_9_MES { get; set; }
        public int POR_10_MES { get; set; }
        public int POR_11_MES { get; set; }
        public int POR_12_MES { get; set; }
        public int MES_INICIAL { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }

    }
}
