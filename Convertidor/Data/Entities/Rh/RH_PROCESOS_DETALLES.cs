using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_PROCESOS_DETALLES
    {
        [Key]
        public int CODIGO_DETALLE_PROCESO { get; set; }
        public int CODIGO_PROCESO { get; set; } 
        public int CODIGO_CONCEPTO { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}

