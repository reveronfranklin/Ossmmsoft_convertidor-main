namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_MAYORES
    {
        public int CODIGO_MAYOR { get; set; }
        public string NUMERO_MAYOR { get; set; } = string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int CODIGO_BALANCE { get; set; }
        public string COLUMNA_BALANCE { get; set; } = string.Empty;
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
