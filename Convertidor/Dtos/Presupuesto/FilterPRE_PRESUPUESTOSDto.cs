﻿using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class FilterPRE_PRESUPUESTOSDto
	{
        public int CodigoPresupuesto { get; set; }
        public string SearchText { get; set; } = string.Empty;
        public int CodigoEmpresa { get; set; }
        public int FinanciadoId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }


    }
}

