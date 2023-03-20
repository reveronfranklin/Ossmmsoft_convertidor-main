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
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal TotalDisponible { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string TotalDisponibleString { get; set; }
        public string TotalPresupuestoString { get; set; }
        public List<GetPRE_V_DENOMINACION_PUCDto> PreDenominacionPuc  { get; set; }
      
    }
}

