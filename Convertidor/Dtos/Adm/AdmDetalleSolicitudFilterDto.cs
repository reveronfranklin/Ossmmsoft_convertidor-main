namespace Convertidor.Dtos.Adm
{
    public class AdmDetalleSolicitudFilterDto
    {
        
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        public int CodigoSolicitud { get; set; }
        public string SearchText { get; set; } 
        
    }
}
