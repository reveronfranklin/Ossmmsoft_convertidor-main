using System;
using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_PROCESOS_DETALLES
    {
        [Key]
        public int CODIGO_DETALLE_PROCESO { get; set; }
        public int CODIGO_PROCESO { get; set; } 
        public int CODIGO_CONCEPTO { get; set; } 

    }
}

