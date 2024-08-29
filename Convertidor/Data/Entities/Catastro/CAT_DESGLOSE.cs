namespace Convertidor.Data.Entities.Catastro
{
    public class CAT_DESGLOSE
    {
        public int CODIGO_DESGLOSE { get; set; }
        public int CODIGO_DESGLOSE_FK { get; set; }
        public int CODIGO_DESGLOSE_PK { get; set; }
        public decimal CODIGO_PARCELA { get; set; }
        public string CODIGO_CATASTRO { get; set; } = string.Empty;
        public string TITULO { get; set; } = string.Empty;
        public int AREA_TERRENO_TOTAL { get; set; }
        public int AREA_CONTRUC_TOTAL { get; set; }
        public int AREA_TRR_TOTAL_VENDI { get; set; }
        public int AREA_TERR_COMUN { get; set; }
        public int AREA_CONTRUC_COMUN { get; set; }
        public int AREA_TERR_SIN_COND { get; set; }
        public int AREA_CONTRUC_SIN_COND { get; set; }
        public int AREA { get; set; }
        public int ESTACIONA_TERR { get; set; }
        public int ESTACIONA_CONTRUC { get; set; }
        public decimal PORCENTAJ_CONDOMINIO { get; set; }
        public int MANUAL_TERRENO { get; set; }
        public int MANUAL_CONSTRUCCION { get; set; }
        public int MALETERO_TERRENO { get; set; }
        public int MALETERO_CONSTRUCCION { get; set; }
        public string OBSERVACION { get; set; } = string.Empty;
        public string NIVEL_ID { get; set; } = string.Empty;
        public string UNIDAD_ID { get; set; } = string.Empty;
        public int TIPO_OPERACION_ID { get; set; }
        public string TIPO_TRANSACCION { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public DateTime EXTRA1 { get; set; }
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public string EXTRA4 { get; set; } = string.Empty;
        public string EXTRA5 { get; set; } = string.Empty;
        public int CODIGO_EMPRESA { get; set; }


    }
}
