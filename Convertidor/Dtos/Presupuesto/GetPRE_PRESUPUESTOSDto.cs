using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class GetPRE_PRESUPUESTOSDto
	{
        public int CodigoPresupuesto { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Ano { get; set; }
        public decimal MontoPresupuesto { get; set; }
        public string FechaDesde { get; set; } = string.Empty;
        public string FechaHasta { get; set; } = string.Empty;
        public string FechaAprobacion { get; set; } = string.Empty;
        public string NumeroOrdenanza { get; set; } = string.Empty;
        public string FechaOrdenanza { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;

        public decimal TotalDisponible { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string TotalDisponibleString { get; set; } =string.Empty;
        public string TotalPresupuestoString { get; set; } = string.Empty;

        public FechaDto FechaDesdeObj { get; set; }
        public FechaDto FechaHastaObj { get; set; }
        public FechaDto FechaAprobacionObj { get; set; }
        public FechaDto FechaOrdenanzaObj { get; set; }

        public List<GetPRE_V_DENOMINACION_PUCDto>? PreDenominacionPuc  { get; set; }
        public List<GetPreDenominacionPucResumenAnoDto>? PreDenominacionPucResumen { get; set; }

    }


}

