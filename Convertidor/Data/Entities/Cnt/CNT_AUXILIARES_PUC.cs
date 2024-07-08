namespace Convertidor.Data.Entities.Cnt
{
    public class CNT_AUXILIARES_PUC
    {
        public int CODIGO_AUXILIAR_PUC { get; set; }
        public int CODIGO_AUXILIAR { get; set; }
        public int CODIGO_PUC { get; set; }
        public string TIPO_DOCUMENTO_ID { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}
