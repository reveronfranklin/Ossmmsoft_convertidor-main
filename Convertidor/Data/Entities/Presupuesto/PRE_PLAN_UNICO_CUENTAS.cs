using System;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_PLAN_UNICO_CUENTAS
	{
		public int CODIGO_PUC { get; set; }
        public string CODIGO_GRUPO { get; set; } = string.Empty;
        public string CODIGO_NIVEL1 { get; set; } = string.Empty;
        public string CODIGO_NIVEL2 { get; set; } = string.Empty;
        public string CODIGO_NIVEL3 { get; set; } = string.Empty;
        public string CODIGO_NIVEL4 { get; set; } = string.Empty;
        public string CODIGO_NIVEL5 { get; set; } = string.Empty;
        public string CODIGO_NIVEL6 { get; set; } = string.Empty;
        public string DENOMINACION { get; set; } = string.Empty;
        public string? DESCRIPCION { get; set; } = string.Empty;
        public DateTime? FECHA_INI { get; set; }
        public DateTime? FECHA_FIN { get; set; }
        public string? EXTRA1 { get; set; } = string.Empty;
        public string? EXTRA2 { get; set; } = string.Empty;
        public string? EXTRA3 { get; set; } = string.Empty;
        public int? USUARIO_INS { get; set; }
        public DateTime? FECHA_INS { get; set; }
        public int? USUARIO_UPD { get; set; }
        public DateTime? FECHA_UPD { get; set; }
        public int? CODIGO_PRESUPUESTO { get; set; }
        public int? CODIGO_EMPRESA { get; set; }
        public int? CODIGO_PUC_FK { get; set; }


    }
}

