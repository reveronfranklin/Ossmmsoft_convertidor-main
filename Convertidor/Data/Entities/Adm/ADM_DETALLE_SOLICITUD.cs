﻿using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_DETALLE_SOLICITUD
    {
        public int CODIGO_DETALLE_SOLICITUD { get; set; }	
        public int CODIGO_SOLICITUD { get; set; }
        public int CANTIDAD { get; set; } 	
        public int CANTIDAD_COMPRADA { get; set; } 
        public int CANTIDAD_ANULADA { get; set; }
        public int UDM_ID { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public int PRECIO_UNITARIO { get; set; } 
        public int POR_DESCUENTO { get; set; } 
        public int MONTO_DESCUENTO { get; set; } 
        public int TIPO_IMPUESTO_ID { get; set; } 
        public int POR_IMPUESTO { get; set; } 
        public int MONTO_IMPUESTO { get; set; }
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

