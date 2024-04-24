namespace Convertidor.Data.Entities.Bm
{
    public class BM_BIENES
    {

        public int CODIGO_BIEN { get; set; }
        public int CODIGO_ARTICULO { get; set; }
        public int CODIGO_PROVEEDOR { get; set; } 
        public int CODIGO_ORDEN_COMPRA { get; set; } 
        public int ORIGEN_ID { get; set; } 
        public DateTime FECHA_FABRICACION { get; set; } 
        public string NUMERO_ORDEN_COMPRA { get; set; } = string.Empty;
        public DateTime FECHA_COMPRA { get; set; } 
        public string NUMERO_PLACA { get; set; } = string.Empty;
        public string NUMERO_LOTE { get; set; } = string.Empty;
        public int VALOR_INICIAL { get; set; }
        public int VALOR_ACTUAL { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public string NUMERO_FACTURA { get; set; } = string.Empty;
        public DateTime FECHA_FACTURA { get; set; }
        public int TIPO_IMPUESTO_ID { get; set; }
      

    }
}

