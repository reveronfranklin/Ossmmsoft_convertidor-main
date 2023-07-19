using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class ListPresupuestoDto
	{
        public int CodigoPresupuesto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Ano { get; set; }
        public List<PreFinanciadoDto>? PreFinanciadoDto { get; set; }
    }
}

