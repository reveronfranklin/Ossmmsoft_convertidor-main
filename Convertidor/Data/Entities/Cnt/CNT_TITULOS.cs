namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_TITULOS
    {
        public int TITULO_ID { get; set; }
        public int TITULO_FK_ID { get; set; }
        public string TITULO { get; set; } = string.Empty;
        public string CODIGO { get; set; }= string.Empty;
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
