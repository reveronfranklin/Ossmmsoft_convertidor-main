namespace Convertidor.Data.Entities.Adm
{
    public class ADM_VAL_CONTRATO
    {
        public int CODIGO_VAL_CONTRATO { get; set; }
        public int CODIGO_CONTRATO { get; set; }
        public string TIPO_VALUACION { get; set; }=string.Empty;
        public DateTime FECHA_VALUACION { get; set; }
        public string NUMERO_VALUACION { get; set; }=string.Empty;
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public DateTime FECHA_APROBACION { get; set; }
        public string NUMERO_APROBACION { get; set; } = string.Empty;
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public string STATUS { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int MONTO{ get; set; }
        public int MONTO_CAUSADO { get; set; }
        public int MONTO_ANULADO { get; set; }
    }
}
