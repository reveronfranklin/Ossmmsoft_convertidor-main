using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_RELACION_CARGOS
	{
        [Key]
        public int CODIGO_RELACION_CARGO { get; set; }
        public int ANO { get; set; }
        public int ESCENARIO { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_CARGO { get; set; }
        public int CANTIDAD { get; set; }
        public decimal SUELDO { get; set; }
        public decimal COMPENSACION { get; set; }
        public decimal PRIMA { get; set; }
        public decimal OTRO { get; set; }
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

