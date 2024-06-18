namespace Convertidor.Dtos.Adm
{
    public class AdmSolicitudesFilterDto
    {
        
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        
        public int CodigoPresupuesto { get; set; }
        public int CodigoSolicitud { get; set; }
        public string SearchText { get; set; } 
      
    }
}
