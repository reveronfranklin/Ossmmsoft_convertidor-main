using System;
using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_PRESUPUESTOS
	{
        [Key]
        public int CODIGO_PRESUPUESTO { get; set; }
        public string DENOMINACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int ANO { get; set; }
        public decimal MONTO_PRESUPUESTO { get; set; }
        public DateTime FECHA_DESDE { get; set; }
        public DateTime FECHA_HASTA { get; set; }
        public DateTime FECHA_APROBACION { get; set; }
        public string NUMERO_ORDENANZA { get; set; } = string.Empty;
        public DateTime FECHA_ORDENANZA { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }

    }
}

