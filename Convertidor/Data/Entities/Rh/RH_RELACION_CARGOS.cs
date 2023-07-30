using System;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Data.Entities.Rh
{
	public class RH_RELACION_CARGOS
	{
        public int CODIGO_RELACION_CARGO { get; set; }
        public int CODIGO_TIPO_NOMINA { get; set; }
        public int CODIGO_ICP { get; set; }
        public int CODIGO_CARGO { get; set; }
        public string CARGO_CODIGO { get; set; } = string.Empty;
        public int CODIGO_PERSONA { get; set; }
        public decimal SUELDO { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public string EXTRA1 { get; set; } = string.Empty;
        public string EXTRA2 { get; set; } = string.Empty;
        public string EXTRA3 { get; set; } = string.Empty;
        public int USUARIO_INS { get; set; }
        public DateTime FECHA_INS { get; set; }
        public int USUARIO_UPD { get; set; }
        public DateTime FECHA_UPD { get; set; }
        public int CODIGO_EMPRESA { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public int CODIGO_RELACION_CARGO_PRE { get; set; }
        public DateTime FECHA_INI_VIGENCIA { get; set; }
        public DateTime FECHA_FIN_VIGENCIA { get; set; }


    }
}

