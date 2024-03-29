﻿namespace Convertidor.Dtos.Presupuesto
{
	public class PreRelacionCargoUpdateDto
	{
        public int CodigoRelacionCargo { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public int CodigoIcp { get; set; }
        public int CodigoCargo { get; set; }
        public int Cantidad { get; set; }
        public decimal Sueldo { get; set; }
        public decimal Compensacion { get; set; }
        public decimal Prima { get; set; }
        public decimal Otro { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}

