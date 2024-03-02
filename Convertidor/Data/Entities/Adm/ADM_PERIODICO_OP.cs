namespace Convertidor.Data.Entities.Adm
{
    public class ADM_PERIODICO_OP
    {
        public int CODIGO_PERIODICO_OP { get; set; }
        public int CODIGO_ORDEN_PAGO { get; set; }
        public int CANTIDAD_PAGO { get; set; }
        public string NUMERO_PAGO { get; set; } = string.Empty;
        public DateTime FECHA_PAGO { get; set; }
        public string MOTIVO { get; set; } = string.Empty;
        public decimal MONTO { get; set; }
        public decimal  MONTO_PAGADO { get; set; }
        public decimal MONTO_ANULADO { get; set; }
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
