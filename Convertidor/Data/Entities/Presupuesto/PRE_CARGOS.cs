using System;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_CARGOS
	{
        public int CODIGO_CARGO { get; set; }
        public int TIPO_PERSONAL_ID { get; set; }
        public int TIPO_CARGO_ID { get; set; }
        public string DENOMINACION { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public int GRADO { get; set; }
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

