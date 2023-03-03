using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class FilterPRE_PRESUPUESTOSDto
	{
        public int CodigoPresupuesto { get; set; }
        public string SearchText { get; set; } = string.Empty;
        public int CodigoEmpresa { get; set; } 
    }
}

