namespace Convertidor.Data.Entities.Adm
{
    public class ADM_IMPUESTOS_OP
    {
        public int CODIGO_IMPUESTO_OP { get; set; }
        public int CODIGO_ORDEN_PAGO { get; set; }
        public int TIPO_IMPUESTO_ID { get; set; }
        public int? POR_IMPUESTO { get; set; }
        public int? MONTO_IMPUESTO { get; set; }
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
