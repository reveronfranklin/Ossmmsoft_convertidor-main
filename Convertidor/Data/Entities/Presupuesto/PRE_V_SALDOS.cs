using System;
namespace Convertidor.Data.Entities.Presupuesto
{
	public class PRE_V_SALDOS
	{
        public int CODIGO_SALDO { get; set; }
        public int ANO { get; set; }
        public int? FINANCIADO_ID { get; set; }
        public int? CODIGO_FINANCIADO { get; set; }
        public string? DESCRIPCION_FINANCIADO { get; set; } = string.Empty;
        public int CODIGO_ICP { get; set; }

        public string CODIGO_SECTOR { get; set; } = string.Empty;
        public string CODIGO_PROGRAMA { get; set; } = string.Empty;
        public string CODIGO_SUBPROGRAMA { get; set; } = string.Empty;
        public string CODIGO_PROYECTO { get; set; } = string.Empty;
        public string CODIGO_ACTIVIDAD { get; set; } = string.Empty;
        public string CODIGO_OFICINA { get; set; } = string.Empty;
        public string? CODIGO_ICP_CONCAT { get; set; } = string.Empty;
        public string DENOMINACION_ICP { get; set; } = string.Empty;
        public string? UNIDAD_EJECUTORA { get; set; } = string.Empty;
        public int CODIGO_PUC { get; set; } 
        public string CODIGO_GRUPO { get; set; } = string.Empty;
        public string CODIGO_PARTIDA { get; set; } = string.Empty;
        public string CODIGO_GENERICA { get; set; } = string.Empty;
        public string CODIGO_ESPECIFICA { get; set; } = string.Empty;
        public string CODIGO_SUBESPECIFICA { get; set; } = string.Empty;
        public string CODIGO_NIVEL5 { get; set; } = string.Empty;
        public string? CODIGO_PUC_CONCAT { get; set; } = string.Empty;
        public string DENOMINACION_PUC { get; set; } = string.Empty;
        public decimal PRESUPUESTADO { get; set; } 
        public decimal ASIGNACION { get; set; }
        public decimal BLOQUEADO { get; set; }
        public decimal MODIFICADO { get; set; }
        public decimal AJUSTADO { get; set; }
        public decimal? VIGENTE { get; set; }
        public decimal COMPROMETIDO { get; set; }
        public decimal? POR_COMPROMETIDO { get; set; }
        public decimal? DISPONIBLE { get; set; }
        public decimal CAUSADO { get; set; }
        public decimal? POR_CAUSADO { get; set; }
        public decimal PAGADO { get; set; }
        public decimal? POR_PAGADO { get; set; }
        public int? CODIGO_EMPRESA { get; set; }
        public int? CODIGO_PRESUPUESTO { get; set; }
        public DateTime? FECHA_SOLICITUD { get; set; }
        public string DESCRIPTIVA_FINANCIADO { get; set; } = string.Empty;


    }
}

