﻿using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class FilterPRE_V_SALDOSDto
    {
       
        public int CodigoEmpresa { get; set; }
        public int AnoDesde { get; set; }
        public int AnoHasta { get; set; }
        public string SearchText { get; set; } = string.Empty;

    }

    public class FilterPeriodo
    {

        public int CodigoPeriodo { get; set; }
       

    }
    public class FilterPresupuestoIpcPuc
    {

        public int CodigoPresupuesto{ get; set; }
        public int CodigoIPC { get; set; }
        public int CodigoPuc { get; set; }
    }


}

