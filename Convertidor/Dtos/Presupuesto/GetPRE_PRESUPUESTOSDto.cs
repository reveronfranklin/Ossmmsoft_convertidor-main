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
        /*public DateTime FechaAprobacion { get; set; }
        public string NumeroOrdenanza { get; set; } = string.Empty;
        public DateTime FechaOrdenanza { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoEmpresa { get; set; }*/
    }
}

