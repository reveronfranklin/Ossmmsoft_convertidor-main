using System;
using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_PROCESOS
    {
        [Key]
        public int CODIGO_PROCESO { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;

    }
}

