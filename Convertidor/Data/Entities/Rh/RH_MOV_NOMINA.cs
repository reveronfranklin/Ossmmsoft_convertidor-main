using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_MOV_NOMINA
	{
        [Key]
        public int CODIGO_MOV_NOMINA { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public int CODIGO_PERSONA { get; set; }
        public int CODIGO_CONCEPTO { get; set; }
        public string COMPLEMENTO_CONCEPTO { get; set; } = string.Empty;
        public string TIPO { get; set; } = string.Empty;
        public int FRECUENCIA_ID { get; set; }
        public decimal MONTO { get; set; }
        public string STATUS { get; set; } = string.Empty;
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

