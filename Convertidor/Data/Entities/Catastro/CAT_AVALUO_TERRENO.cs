namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_AVALUO_TERRENO
    {
        public int CODIGO_AVALUO_TERRENO { get; set; }
        public int CODIGO_FICHA { get; set; }
        public DateTime ANO_AVALUO { get; set; }
        public int UNIDAD_MEDIDA_ID { get; set; }
        public int AREA_M2 { get; set; }
        public decimal VALOR_UNITARIO { get; set; }
        public int VALOR_AJUSTADO { get; set; }
        public int FACTOR_AJUSTE { get; set; }
        public int FACTOR_FRENTE { get; set; }
        public int FACTOR_FORMA { get; set; }
        public int FACTOR_ESQUINA { get; set; }
        public int FACTOR_PROF { get; set; }
        public int FACTOR_AREA { get; set; }
        public decimal VALOR_MODIFICADO { get; set; }
        public int AREA_TOTAL { get; set; }
        public decimal MONTO_AVALUO { get; set; }
        public string OBSERVACIONES { get; set; } = string.Empty;
        public int INCREMENTO_ESQUINA { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PARCELA { get; set; }
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
        public int CODIGO_ZONIFICACION { get; set; } 
        public int FRENTE_PARCELA { get; set; }
        public int CODIGO_VIALIDAD_PRINCIPAL { get; set; }
        public int CODIGO_VIALIDAD_ADYACENTE1 { get; set; }
        public int CODIGO_VIALIDAD_ADYACENTE2 { get; set; }
        public int VIALIDAD1 { get; set; }
        public int VIALIDAD2 { get; set; }
        public int VIALIDAD3 { get; set; }
        public int VIALIDAD4 { get; set; }
        public int UBICACION_TERRENO { get; set; }
        public int CODIGO_VIALIDAD1 { get; set; }
        public int CODIGO_VIALIDAD2 { get; set; }
        public int CODIGO_VIALIDAD3 { get; set; }
        public int CODIGO_VIALIDAD4 { get; set; }
        public int FACTOR_PROFUNDIDAD { get; set; }
        public decimal MONTO_TOTAL_AVALUO { get; set; }
    }
}
