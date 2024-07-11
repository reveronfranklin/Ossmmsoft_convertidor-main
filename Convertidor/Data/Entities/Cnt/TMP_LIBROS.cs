namespace Convertidor.Data.Entities.Cnt
{
    public class TMP_LIBROS
    {
        public int CODIGO_LIBRO { get; set; }
        public int CODIGO_CUENTA_BANCO { get; set; }
        public DateTime FECHA_LIBRO { get; set; }
        public string STATUS { get; set; }
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
