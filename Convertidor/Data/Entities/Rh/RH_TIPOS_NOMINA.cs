using System;
namespace Convertidor.Data.Entities.Rh
{
	public class RH_TIPOS_NOMINA
	{


        public int CODIGO_TIPO_NOMINA { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public string SIGLAS_TIPO_NOMINA { get; set; } = string.Empty;
        public int FRECUENCIA_PAGO_ID { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public Decimal SUELDO_MINIMO { get; set; }
        public int CODIGO_PRESUPUESTO_ACTUAL { get; set; }

    }
}