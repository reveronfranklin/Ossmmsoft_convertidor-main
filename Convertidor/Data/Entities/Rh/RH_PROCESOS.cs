using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_PROCESOS
    {
        [Key]
        public int CODIGO_PROCESO { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
    }
}

