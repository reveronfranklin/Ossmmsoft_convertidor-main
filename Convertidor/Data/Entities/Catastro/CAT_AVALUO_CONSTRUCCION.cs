namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_AVALUO_CONSTRUCCION
    {
        public int CODIGO_AVALUO_CONSTRUCCION { get; set; }
        public int CODIGO_FICHA { get; set; }
        public DateTime ANO_AVALUO { get; set; }
        public int PLANTA_ID { get; set; }
        public int UNIDAD_MEDIDA_ID { get; set; }
        public int VALOR_MEDIDA { get; set; }
        public int FACTOR_DEPRECIACION { get; set; }
        public int VALOR_MODIFICADO { get; set; }
        public int AREA_TOTAL { get; set; }
        public int MONTO_AVALUO { get; set; }
        public string OBSERVACIONES { get; set; } = string.Empty;
        public int VALOR_REPOSICION { get; set; }
        public int AREA_CONSTRUCCION { get; set; }
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
        public string TIPOLOGIA { get; set; } = string.Empty;
        public int FRENTE_PARCELA { get; set; }
        public int MONTO_COMPLEMENTO { get; set; }
        public int MONTO_COMPLEMENTO_USUARIO { get; set; }
        public int MONTO_TOTAL_AVALUO { get; set; }
    }
}
