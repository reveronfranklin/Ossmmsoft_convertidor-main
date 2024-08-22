namespace Convertidor.Dtos.Adm
{
    public class PreCompromisosFilterDto
    {
        
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        
        public int CodigoPresupuesto { get; set; }
        public int CodigoCompromiso { get; set; }
        public string SearchText { get; set; } 
        public string Status { get; set; } 
      
    }
}
