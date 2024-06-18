namespace Convertidor.Data.Entities.Bm
{
    public class BM_DETALLE_ARTICULOS
    {

        public int CODIGO_DETALLE_ARTICULO { get; set; }
        public int CODIGO_ARTICULO { get; set; }
        public int TIPO_ESPECIFICACION_ID { get; set; } 
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

