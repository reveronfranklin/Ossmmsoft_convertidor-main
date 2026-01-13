namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_D_VALOR_CONSTRUCCION
    {
        public int CODIGO_PARCELA { get; set; }
        public int CODIGO_D_VALOR_CONSTRUCCION { get; set; }
        public int CODIGO_VALOR_CONSTRUCCION { get; set; }
        public int CODIGO_INMUEBLE { get; set; }
        public string CODIGO_CATASTRO { get; set; } = string.Empty;
        public int ESTRUCTURA_NIVEL1_ID { get; set; }
        public int ESTRUCTURA_NIVEL2_ID { get; set; }
        public int ESTRUCTURA_NIVEL3_ID { get; set; }
        public int ESTRUCTURA_NIVEL4_ID { get; set; }
        public string ESTRUCTURA_DESCRIPTIVA { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string EXTRA4 { get; set; } = string.Empty;
        public string EXTRA5 { get; set; } = string.Empty;
        public string EXTRA6 { get; set; } = string.Empty;
        public string EXTRA7 { get; set; } = string.Empty;
        public string EXTRA8 { get; set; } = string.Empty;
        public string EXTRA9 { get; set; } = string.Empty;
        public string EXTRA10 { get; set; } = string.Empty;
        public string EXTRA11 { get; set; } = string.Empty;
        public string EXTRA12 { get; set; } = string.Empty;
        public string EXTRA13 { get; set; } = string.Empty;
        public string EXTRA14 { get; set; } = string.Empty;
        public string EXTRA15 { get; set; } = string.Empty;
        public int VALOR_COMPLEMENTARIO { get; set; }
    }
}
