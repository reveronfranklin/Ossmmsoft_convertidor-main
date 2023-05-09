using System;
namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_V_DOC_BLOQUEADO
	{
        public int CODIGO_SALDO { get; set; }
        public int CODIGO_PRESUPUESTO { get; set; }
        public string DESCRIPCION { get; set; } = string.Empty;
        public DateTime FECHA { get; set; }
        public string NUMERO { get; set; } = string.Empty;
        public string NUMERO1 { get; set; } = string.Empty;
        public string MOTIVO { get; set; } = string.Empty;
        public decimal MONTO { get; set; }

    }
}

