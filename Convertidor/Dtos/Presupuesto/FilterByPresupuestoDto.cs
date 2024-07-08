namespace Convertidor.Dtos.Presupuesto
{
	public class FilterByPresupuestoDto
	{
		
		public int PageSize { get; set; } 
		public int PageNumber { get; set; }
		public int CodigoPresupuesto { get; set; }
        public int? CodigoIcp { get; set; }
        public string SearchText { get; set; }
    }
}

