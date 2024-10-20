namespace Convertidor.Dtos.Presupuesto
{
    public class FilterBySolicitud
    {
        public int CodigoSolModificacion { get; set; }
        public FilterDto filterDto { get; set; }
    }
      
    public class FilterDto 
    {
        public int CodigoSolModificacion { get; set; }
        public string DePara { get; set; }
    }
    public class FilterReporteBySolicitud
    {
        public int CodigoSolModificacion { get; set; }
      
    }
    
}
        
    

