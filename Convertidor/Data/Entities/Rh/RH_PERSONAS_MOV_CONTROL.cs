using System;
using static NPOI.HSSF.Record.UnicodeString;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_PERSONAS_MOV_CONTROL
    {
        
        public int CODIGO_PERSONA_MOV_CTRL { get; set; }
        public int CODIGO_PERSONA { get; set; }
        public int CODIGO_CONCEPTO { get; set; }
        public int CONTROL_APLICA { get; set; }
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

