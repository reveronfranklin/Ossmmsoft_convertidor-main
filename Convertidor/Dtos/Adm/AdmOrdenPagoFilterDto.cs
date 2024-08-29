namespace Convertidor.Dtos.Adm
{
    public class AdmOrdenPagoFilterDto
    {
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string SearchText { get; set; } 
        public string Status { get; set; } 
      
    }
}
