﻿using System;
namespace Convertidor.Dtos.Rh
{
	public class RhRelacionCargoDto
	{
        public int CodigoRelacionCargo { get; set; }
        public int CodigoCargo { get; set; }
        public string DenominacionCargo { get; set; } = string.Empty;
        public int TipoNomina { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoPersona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int Cedula { get; set; } 
        public decimal Sueldo { get; set; }
        public string FechaIni { get; set; } = string.Empty;
        public string FechaFin { get; set; } = string.Empty;
        public FechaDto FechaIniObj { get; set; }
        public FechaDto FechaFinObj { get; set; }
        public int CodigoRelacionCargoPre { get; set; }
        public string searchText { get { return DenominacionCargo + Nombre + Apellido + Cedula; } }
    }
}
