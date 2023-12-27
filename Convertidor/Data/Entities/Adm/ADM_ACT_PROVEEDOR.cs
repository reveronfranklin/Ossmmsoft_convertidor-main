using System;
using System.ComponentModel.DataAnnotations;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Data.Entities.ADM
{
	public class ADM_ACT_PROVEEDOR
	{


        [Key]
        public int CODIGO_ACT_PROVEEDOR  { get; set; } 
        public int CODIGO_PROVEEDOR  { get; set; } 
        public int ACTIVIDAD_ID { get; set; } 
		public DateTime FECHA_INI { get; set; } 
        public DateTime FEHA_FIN { get; set; } 
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

