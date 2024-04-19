namespace Convertidor.Data.Entities.Presupuesto
{
    public class PRE_METAS
    {
        public int CODIGO_META { get; set; }
        public int CODIGO_PROYECTO { get; set; }
        public int NUMERO_META { get; set; }
        public string DENOMINACION_META { get; set; } = string.Empty;
        public int UNIDAD_MEDIDA_ID { get; set; }
        public int CANTIDAD_META { get; set; }
        public int COSTO_META { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int CANTIDAD_PRIMER_TRIMESTRE { get; set; }
        public int CANTIDAD_SEGUNDO_TRIMESTRE { get; set; }
        public int CANTIDAD_TERCER_TRIMESTRE { get; set; }
        public int CANTIDAD_CUARTO_TRIMESTRE { get; set; }

    }
}
