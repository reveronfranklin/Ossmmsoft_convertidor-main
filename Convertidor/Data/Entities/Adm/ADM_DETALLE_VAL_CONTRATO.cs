namespace Convertidor.Data.Entities.Adm
{
    public class ADM_DETALLE_VAL_CONTRATO
    {
        public int CODIGO_DETALLE_VAL_CONTRATO { get; set; }
        public int CODIGO_CONTRATO { get; set; }
        public int CODIGO_VAL_CONTRATO { get; set; }
        public int CONCEPTO_ID { get; set; }
        public string COMPLEMENTO_CONCEPTO { get; set; } = string.Empty;
        public int POR_CONCEPTO { get; set; }
        public int MONTO_CONCEPTO { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
    }
}
