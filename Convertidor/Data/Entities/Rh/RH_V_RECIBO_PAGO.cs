namespace Convertidor.Data.Entities.Rh
{
    public class RH_V_RECIBO_PAGO
    {
        public DateTime FECHA_PERIODO_NOMINA { get; set; }
        public DateTime FECHA_NOMINA { get; set; }
        public int CODIGO_PERIODO { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public string CODIGO_ICP_CONCAT { get; set; } = string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public string DENOMINACION_CARGO { get; set; } = string.Empty;
        public int CEDULA { get; set; }
        public string NOMBRE { get; set; } = string.Empty;
        public string NO_CUENTA { get; set; } = string.Empty;
        public string DESCRIPCION_TIPO_NOMINA { get; set; } = string.Empty;
        public string CODIGO_CONCEPTO { get; set; } = string.Empty;
        public int CODIGO_CONCEPTO_ID { get; set; }
        public string TIPO_MOV_CONCEPTO { get; set; } = string.Empty;
        public string DENOMINACION_CONCEPTO { get; set; } = string.Empty;
        public string TIPO_CONCEPTO { get; set; } = string.Empty;
        public string PORCENTAJE { get; set; } = string.Empty;
        public decimal ASIGNACION { get; set; }
        public decimal DEDUCCION { get; set; }
        public decimal MONTO { get; set; }
        public decimal SUELDO_BASE { get; set; }
        public string TIPO_SUELDO { get; set; } = string.Empty;
        public decimal SUELDO { get; set; }
        public string CODIGO_TIPO_SUELDO_DESC { get; set; } = string.Empty;
        public string COMPLEMENTO_CONCEPTO { get; set; } = string.Empty;
        public int CODIGO_PERSONA { get; set; }
        public string MODULO { get; set; } = string.Empty;
        public string CODIGO_IDENTIFICADOR { get; set; } = string.Empty;
        public int CODIGO_EMPRESA { get; set; }
    }
}
