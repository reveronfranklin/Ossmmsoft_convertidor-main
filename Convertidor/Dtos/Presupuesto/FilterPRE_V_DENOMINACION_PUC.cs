using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class FilterPRE_V_DENOMINACION_PUC
    {
       
        public int CodigoEmpresa { get; set; }
        public int AnoDesde { get; set; } 
        public int AnoHasta { get; set; } 
        public string SearchText { get; set; } = string.Empty;

    }
    public class FilterPreVDenominacionPuc
    {

        public int CodigoPresupuesto { get; set; }
      

    }
}

