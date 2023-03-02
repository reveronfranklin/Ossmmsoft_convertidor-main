using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class FilterPRE_PRESUPUESTOSDto
	{
        public int CODIGO_PRESUPUESTO { get; set; }
        public string SearchText { get; set; } = string.Empty;
        public int CODIGO_EMPRESA { get; set; } 
    }
}

