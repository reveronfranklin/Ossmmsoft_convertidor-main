namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_SALDOS
    {
        public int CODIGO_SALDO { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public int CODIGO_MAYOR { get; set; }
        public int CODIGO_AUXILIAR { get; set; }
        public int DEBITOS { get; set; }
        public int CREDITOS { get; set; }
        public decimal MONTO { get; set; }
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
